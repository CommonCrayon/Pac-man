using UnityEngine;

public class BorderElements : MonoBehaviour
{
    private float speed = 0.75f;

    private Vector3 BL = new Vector3(-935, -485, 0);
    private Vector3 TL = new Vector3(-935, 515, 0);
    private Vector3 TR = new Vector3(915, 515, 0);
    private Vector3 BR = new Vector3(915, -485, 0);

    private void Update()
    {
        if (transform.localPosition.y == -485 && transform.localPosition.x != -935)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, BL, speed);
        }
        else if (transform.localPosition.x == -935 && transform.localPosition.y != 515)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TL, speed);
        }
        else if (transform.localPosition.y == 515 && transform.localPosition.x != 915)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TR, speed);
        }
        else if (transform.localPosition.x == 915 && transform.localPosition.y != -485)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, BR, speed);
        }
    }
}
