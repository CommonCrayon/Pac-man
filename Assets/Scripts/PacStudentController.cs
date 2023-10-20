using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float lerpSpeed = 8f;

    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 lastInput = Vector3.zero;

    private void Start()
    {
        currentPosition = transform.position;
        targetPosition = currentPosition;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");



        if (horizontal != 0 || vertical != 0)
        {
            Vector3 moveDirection = Vector3.zero;

            // Taking the Input of which every was last registered
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
                moveDirection = new Vector3(horizontal, 0, 0);
            else
                moveDirection = new Vector3(0, vertical, 0);

            // Save the last move direction
            lastInput = moveDirection;

            // Calculate the new target position
            targetPosition = GetValidTargetPosition(currentPosition + moveDirection);
        }
        else
        {
            // If no new input, repeat the last move direction
            targetPosition = GetValidTargetPosition(currentPosition + lastInput);
        }

        // Lerp towards the target position
        transform.position = Vector3.Lerp(currentPosition, targetPosition, lerpSpeed * Time.deltaTime);
        currentPosition = transform.position;
    }



    // Validating that the Target Position is on grid
    private Vector3 GetValidTargetPosition(Vector3 newPosition)
    {
        float x = Mathf.Round((newPosition.x - 0.5f) / 1) + 0.5f; // 0.5f to adjust for grid offset
        float y = Mathf.Round(newPosition.y / 1);
        return new Vector3(x, y, newPosition.z);
    }
}
