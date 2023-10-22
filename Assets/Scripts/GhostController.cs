using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isDead = false;

    private float lerpSpeed = 5f;
    private bool isLerping = false;

    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private Vector3 previousDirection;


    // MOVEMENT ====================================================================================
    private void Start()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;
        previousDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (!isLerping)
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
            if (ValidateTargetPosition(leftPosition) && leftPosition != lastPosition && previousDirection != Vector3.right)
            {
                possibleDirections.Add(leftPosition);
            }
            if (ValidateTargetPosition(downPosition) && downPosition != lastPosition && previousDirection != Vector3.up)
            {
                possibleDirections.Add(downPosition);
            }

            // Choose a random direction from the valid ones
            if (possibleDirections.Count > 0)
            {
                targetPosition = possibleDirections[Random.Range(0, possibleDirections.Count)];
                lastPosition = currentPosition;

                Vector3 dir = (targetPosition - currentPosition).normalized;
                previousDirection = new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));

                StartCoroutine(LerpToTarget(targetPosition));
            }
        }
    }

    IEnumerator LerpToTarget(Vector3 target)
    {
        isLerping = true;


        while (Vector3.Distance(transform.position, targetPosition) > 0.1)
        {
            // Lerp towards the target position
            transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpSpeed * Time.deltaTime);
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
            }
            else
            {
                GameManager.instance.KillPacStudent();
            }
        }
    }

    public void PlayGhostScared()
    {
        Debug.Log(animator);
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
    }
}
