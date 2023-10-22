using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public enum Ghost
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    public Ghost ghostNumber;

    private int ghost4Tracker = 1;

    [SerializeField] private Animator animator;
    private bool isDead = false;

    private float lerpSpeed = 5f;
    private bool isLerping = false;

    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private Vector3 previousDirection;

    private Vector3 RespawnLocation;


    // MOVEMENT ====================================================================================
    private void Start()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;
        previousDirection = Vector3.zero;

        RespawnLocation = currentPosition;
    }

    private void FixedUpdate()
    {
        if (!isLerping && !isDead)
        {
            // Calculate the new target position
            List<Vector3> possibleDirections = new List<Vector3>();
            Vector3 upPosition = GetTargetPosition(currentPosition + Vector3.up);
            Vector3 downPosition = GetTargetPosition(currentPosition + Vector3.down);
            Vector3 rightPosition = GetTargetPosition(currentPosition + Vector3.right);
            Vector3 leftPosition = GetTargetPosition(currentPosition + Vector3.left);

            if (ValidateTargetPosition(upPosition) && upPosition != lastPosition && previousDirection != Vector3.down)
            {
                possibleDirections.Add(upPosition);
            }
            if (ValidateTargetPosition(rightPosition) && rightPosition != lastPosition && previousDirection != Vector3.left)
            {
                possibleDirections.Add(rightPosition);
            }
            if (ValidateTargetPosition(downPosition) && downPosition != lastPosition && previousDirection != Vector3.up)
            {
                possibleDirections.Add(downPosition);
            }
            if (ValidateTargetPosition(leftPosition) && leftPosition != lastPosition && previousDirection != Vector3.right)
            {
                possibleDirections.Add(leftPosition);
            }


            // Choose a random direction from the valid ones
            if (possibleDirections.Count > 0)
            {
                //targetPosition = possibleDirections[Random.Range(0, possibleDirections.Count)];
                targetPosition = GetTargetPosition(possibleDirections);

                lastPosition = currentPosition;

                Vector3 dir = (targetPosition - currentPosition).normalized;
                previousDirection = new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));

                StartCoroutine(LerpToTarget(targetPosition));
            }
            else
            {
                StartCoroutine(LerpToTarget(lastPosition));
            }
        }
    }


    private Vector3 GetTargetPosition(List<Vector3> possibleDirections)
    {
        // If the Ghost is still in the spawn point
        if (Vector3.Distance(transform.position, Vector3.zero) < 3f)
        {
            if (Vector3.Distance(transform.position + Vector3.up, Vector3.zero) > Vector3.Distance(transform.position + Vector3.down, Vector3.zero))
            {
                // IF direction possible return up
                foreach (var dir in possibleDirections)
                {
                    if (((transform.position - dir).normalized) == (transform.position - (transform.position + Vector3.up)))
                    {
                        return transform.position + Vector3.up;
                    }
                }

            }
            else
            {
                // IF direction possible return down
                foreach (var dir in possibleDirections)
                {
                    if (((transform.position - dir).normalized) == (transform.position - (transform.position + Vector3.down)))
                    {
                        return transform.position + Vector3.down;
                    }
                }
            }
        }

        // Ghost One Goes to the Furthest possible direction of the player.
        else if (ghostNumber == Ghost.One || GameManager.instance.GhostKillable) // If ghost scared/recovering use Ghost 1 behaviour
        {
            Vector3 furthestTarget = possibleDirections[0];

            foreach (var dir in possibleDirections)
            {
                if (Vector3.Distance(dir, GameManager.instance.PacStudent.transform.position) >
                    Vector3.Distance(furthestTarget, GameManager.instance.PacStudent.transform.position)
                    )
                {
                    furthestTarget = dir;
                }
            }
            return furthestTarget;
        }

        // Ghost Two goes to closest possible direction of the player.
        else if (ghostNumber == Ghost.Two)
        {
            Vector3 closestTarget = possibleDirections[0];

            foreach (var dir in possibleDirections)
            {
                if (Vector3.Distance(dir, GameManager.instance.PacStudent.transform.position) <
                    Vector3.Distance(closestTarget, GameManager.instance.PacStudent.transform.position)
                    )
                {
                    closestTarget = dir;
                }
            }
            return closestTarget;
        }

        // Moves in a randomly selected valid direction
        else if (ghostNumber == Ghost.Three)
        {
            return possibleDirections[Random.Range(0, possibleDirections.Count)];
        }

        // Move clockwise around the map, following the outside wall
        else if (ghostNumber == Ghost.Four)
        {
            Vector3 TL = new Vector3(-12.5f, 13f, 0f);
            Vector3 TR = new Vector3(12.5f, 13f, 0f);
            Vector3 BR = new Vector3(12.5f, -13f, 0f);
            Vector3 BL = new Vector3(-12.5f, -13f, 0f);

            if (ghost4Tracker == 1)
            {
                Vector3 closestTarget = possibleDirections[0];

                foreach (var dir in possibleDirections)
                {
                    if (Vector3.Distance(dir, TL) < Vector3.Distance(closestTarget, TL))
                    {
                        closestTarget = dir;
                    }
                }

                if (Vector3.Distance(TL, transform.position) < 1)
                    ghost4Tracker = 2;

                return closestTarget;
            }

            else if (ghost4Tracker == 2)
            {
                Vector3 closestTarget = possibleDirections[0];

                foreach (var dir in possibleDirections)
                {
                    if (Vector3.Distance(dir, TR) < Vector3.Distance(closestTarget, TR))
                    {
                        closestTarget = dir;
                    }
                }

                if (Vector3.Distance(TR, transform.position) < 1)
                    ghost4Tracker = 3;

                return closestTarget;
            }

            else if (ghost4Tracker == 3)
            {
                Vector3 closestTarget = possibleDirections[0];

                foreach (var dir in possibleDirections)
                {
                    if (Vector3.Distance(dir, BR) < Vector3.Distance(closestTarget, BR))
                    {
                        closestTarget = dir;
                    }
                }

                if (Vector3.Distance(BR, transform.position) < 1)
                    ghost4Tracker = 4;

                return closestTarget;
            }

            else if (ghost4Tracker == 4)
            {
                Vector3 closestTarget = possibleDirections[0];

                foreach (var dir in possibleDirections)
                {
                    if (Vector3.Distance(dir, BL) < Vector3.Distance(closestTarget, BL))
                    {
                        closestTarget = dir;
                    }
                }

                if (Vector3.Distance(BL, transform.position) < 1)
                    ghost4Tracker = 1;

                return closestTarget;
            }
        }

        // Return current position, sometimes due to level generator making fake checks
        return transform.position;
    }




    IEnumerator LerpToTarget(Vector3 target)
    {
        isLerping = true;


        while (Vector3.Distance(transform.position, target) > 0.1)
        {
            // Lerp towards the target position
            transform.position = Vector3.Lerp(currentPosition, target, lerpSpeed * Time.deltaTime);
            currentPosition = transform.position;

            yield return null;
        }


        isLerping = false;
    }

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
            if (hitInfo.collider.CompareTag("Wall") || hitInfo.collider.CompareTag("GhostBlock"))
            {
                // Hit wall so return false
                return false;
            }
        }
        return true;
    }

    IEnumerator MoveToRespawnLocation()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < 3f)
        {
            // Calculate the interpolation factor (t) based on the elapsed time and duration.
            float t = elapsedTime / 3f;

            // Lerp the position gradually over time.
            transform.position = Vector3.Lerp(initialPosition, RespawnLocation, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final position is the RespawnLocation.
        transform.position = RespawnLocation;

        currentPosition = transform.position;
        targetPosition = currentPosition;
        previousDirection = Vector3.zero;
    }

    // MOVEMENT ====================================================================================


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("PacStudent"))
        {
            if (GameManager.instance.GhostKillable)
            {
                isDead = true;
                GameManager.instance.AddScore(300);
                StartCoroutine(GhostRecover());
                StartCoroutine(MoveToRespawnLocation());
            }
            else
            {
                GameManager.instance.KillPacStudent();
            }
        }
    }

    public void PlayGhostScared()
    {
        animator.Play("CatScared");
    }

    public void PlayGhostRecovering()
    {
        if (!isDead)
            animator.Play("CatRecovering");
    }

    public void PlayGhostNormal()
    {
        if (!isDead)
            animator.Play("Up");
    }

    private IEnumerator GhostRecover()
    {
        animator.Play("CatDead");

        yield return new WaitForSeconds(3);

        animator.Play("CatRecovering");

        yield return new WaitForSeconds(2);

        animator.Play("Up");

        isDead = false;
    }
}
