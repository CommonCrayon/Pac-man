using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPacStudent : MonoBehaviour
{
    private int MoveCount = 0;
    private float speed = 3f;


    private void FixedUpdate()
    {
        if (MoveCount < 150) MoveUp();
        else if (MoveCount < 200) MoveRight();
        else if (MoveCount < 250) MoveDown();
        else if (MoveCount < 300) MoveRight();
        else if (MoveCount < 350) MoveDown();
        else if (MoveCount < 400) MoveLeft();
        else if (MoveCount < 450) MoveDown();
        else if (MoveCount < 500) MoveLeft();
        else MoveCount = 0;
    }

    private void MoveUp()
    {
        transform.position += Vector3.up * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        MoveCount++;
    }

    private void MoveDown()
    {
        transform.position += Vector3.down * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        MoveCount++;
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        MoveCount++;
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, 270);
        MoveCount++;
    }
}
