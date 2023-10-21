using System.Collections;
using TMPro;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Vector3 respawnPosition;

    private float lerpSpeed = 5f;
    private bool isLerping = false;

    private Vector3 targetPosition;
    private Vector3 currentPosition;

    private Vector3 playerInput = Vector3.zero;
    private Vector3 lastInput = Vector3.zero;

    private Animator animator;

    [SerializeField] private AudioSource playerWalkingAS;
    [SerializeField] private ParticleSystem smokePS;

    [SerializeField] private AudioSource wallCollideAS;
    [SerializeField] private ParticleSystem wallCollidePS;

    [SerializeField] private ParticleSystem deathPS;

    private void Start()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;

        respawnPosition = transform.position;

        animator = GetComponent<Animator>();

        playerWalkingAS.Stop();
        smokePS.Stop();
        animator.SetBool("Lerping", false);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");



        if (horizontal != 0 || vertical != 0)
        {
            // Taking the Input of the last button registered
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                playerInput = new Vector3(horizontal, 0, 0);
            else
                playerInput = new Vector3(0, vertical, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!isLerping)
        {
            // Calculate the new target position
            targetPosition = GetTargetPosition(currentPosition + playerInput);
            Vector3 lastInputTargetPosition = GetTargetPosition(currentPosition + lastInput);


            // If wall is in the way
            if (ValidateTargetPosition(targetPosition))
            {
                StartCoroutine(LerpToTarget(targetPosition));
                lastInput = playerInput;

                StartMovingPacStudent();
            }
            else if (ValidateTargetPosition(lastInputTargetPosition))
            {
                targetPosition = lastInputTargetPosition;

                StartCoroutine(LerpToTarget(targetPosition));
                playerInput = lastInput;

                StartMovingPacStudent();
            }
            // When player is forced to stop
            else
            {
                StopMovingPacStudent();
            }
        }
    }

    public void Respawn()
    {
        animator.Play("PacStudentDeath");
        deathPS.Emit(10000);

        playerInput = Vector3.zero;
        lastInput = Vector3.zero;

        // Freeze for 1 second
        StartCoroutine(FreezeForOneSecond());
    }

    private IEnumerator FreezeForOneSecond()
    {
        yield return new WaitForSeconds(1.0f);

        transform.position = respawnPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        currentPosition = respawnPosition;
        targetPosition = respawnPosition;
    }


    /// <summary>
    /// Lerp the PacStudent to the Target Position
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    IEnumerator LerpToTarget(Vector3 target)
    {
        isLerping = true;


        while (Vector3.Distance(transform.position, targetPosition) > 0.1)
        {
            // Lerp towards the target position
            transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpSpeed * Time.deltaTime);
            currentPosition = transform.position;

            RotatePacStudent((targetPosition - currentPosition).normalized);

            yield return null;
        }


        isLerping = false;
    }



    /// <summary>
    /// Gets the Target Position to make sure its on the grid
    /// </summary>
    /// <param name="newPosition">Next Target Position</param>
    /// <returns></returns>
    private Vector3 GetTargetPosition(Vector3 newPosition)
    {
        float x = Mathf.Round((newPosition.x - 0.5f) / 1) + 0.5f; // 0.5f to adjust for grid offset
        float y = Mathf.Round(newPosition.y / 1);
        return new Vector3(x, y, newPosition.z);
    }

    private bool ValidateTargetPosition(Vector3 targetPosition)
    {
        Ray ray = new Ray(currentPosition, (targetPosition - currentPosition).normalized);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1))
        {
            if (hitInfo.collider.CompareTag("Wall"))
            {
                // Play wall collision sounds and particles immediately
                if (!wallCollideAS.isPlaying)
                    wallCollideAS.Play();

                if (!wallCollidePS.isPlaying)
                    wallCollidePS.Emit(25);

                // Hit wall so return false
                return false;
            }

            else if (hitInfo.collider.CompareTag("Teleport"))
            {
                Vector3 newPosition = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
                transform.position = newPosition;

                currentPosition = transform.position;
                this.targetPosition = newPosition;
                return true;
            }

            else if (hitInfo.collider.CompareTag("NormalPellet"))
            {
                Destroy(hitInfo.collider.gameObject, 0.5f);
                GameManager.instance.AddScore(10);
            }

            else if (hitInfo.collider.CompareTag("PowerPellet"))
            {
                Destroy(hitInfo.collider.gameObject, 0.5f);
                GameManager.instance.PowerPelletActivate();
            }
        }
        return true;
    }


    /// <summary>
    /// Rotate PacStudent in the correct direction
    /// </summary>
    /// <param name="direction">Direction PacStudent is moving</param>
    private void RotatePacStudent(Vector3 direction)
    {
        if (direction.x > 0.25)
            transform.rotation = Quaternion.Euler(0, 0, 270);
        else if (direction.x < -0.25)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (direction.y < -0.25)
            transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (direction.y > 0.25)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    private void StartMovingPacStudent()
    {
        // Play wall collision sounds and particles immediately
        if (wallCollideAS.isPlaying)
            wallCollideAS.Stop();

        if (wallCollidePS.isPlaying)
            wallCollidePS.Stop();

        // Update Animations, Audio and Particles
        if (!animator.GetBool("Lerping"))
        {
            animator.SetBool("Lerping", true);
        }

        if (!playerWalkingAS.isPlaying)
        {
            playerWalkingAS.Play();
        }

        if (!smokePS.isPlaying)
        {
            smokePS.Play();
        }
    }

    private void StopMovingPacStudent()
    {
        // Update Animations, Audio and Particles
        if (animator.GetBool("Lerping"))
        {
            animator.SetBool("Lerping", false);
        }

        if (playerWalkingAS.isPlaying)
        {
            playerWalkingAS.Stop();
        }

        if (smokePS.isPlaying)
        {
            smokePS.Stop();
        }
    }
}
