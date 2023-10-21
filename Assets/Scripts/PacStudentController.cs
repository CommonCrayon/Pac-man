using System.Collections;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private float lerpSpeed = 5f;
    private bool isLerping = false;

    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 previousPosition;

    private Vector3 playerInput = Vector3.zero;

    private Animator animator;
    private AudioSource audioSource;
    private ParticleSystem smokeParticleSystem;

    private void Start()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        smokeParticleSystem = GetComponent<ParticleSystem>();
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

            // If wall in the way
            if (ValidateTargetPosition(targetPosition))
            {
                StartCoroutine(LerpToTarget(targetPosition));

                // Update Animations, Audio and Particles
                if (!animator.GetBool("Lerping"))
                {
                    animator.SetBool("Lerping", true);
                }

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                if (!smokeParticleSystem.isPlaying)
                {
                    smokeParticleSystem.Play();
                }
            }
            else
            {
                // Update Animations, Audio and Particles
                if (animator.GetBool("Lerping"))
                {
                    animator.SetBool("Lerping", false);
                }

                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                if (smokeParticleSystem.isPlaying)
                {
                    smokeParticleSystem.Stop();
                }
            }
        }


        // Calculate the speed based on the distance traveled during lerping
        float speed = Vector3.Distance(previousPosition, transform.position) / Time.deltaTime;

        // Update the previous position
        previousPosition = transform.position;
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
        if (Input.GetKey(KeyCode.Space)) // TODO: Replace with wall collision code
        {
            return false;
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
}
