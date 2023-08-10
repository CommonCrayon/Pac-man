using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudent : MonoBehaviour
{
    // For preview purposes:
    public bool PlayDeath = false;
    private int MoveCount = 0;

    // Private Variables
    private float maxSpeed = 3f;
    private Vector3 movementVector = Vector3.up;
    //private bool isDead = false;

    private void Start()
    {
        if (PlayDeath)
            KillPacStudent();
    }

    private void FixedUpdate()
    {
        if (PlayDeath) return;


        if (MoveCount < 100)
        {
            transform.position += Vector3.up * maxSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            MoveCount++;
        }
        else if (MoveCount < 200) 
        {
            transform.position += Vector3.right * maxSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 270);
            MoveCount++;
        }
        else if (MoveCount < 300)
        {
            transform.position += Vector3.down * maxSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            MoveCount++;
        }
        else if (MoveCount < 400)
        {
            transform.position += Vector3.left * maxSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            MoveCount++;
        }
        else
        {
            MoveCount = 0;
        }

        /* 
         * Movement Code Test
         * 
        
        if (isDead) return;

        // For testing death animation
        else if (Input.GetKey(KeyCode.Q))
        {
            KillPacStudent();
            return;
        }

        else if (Input.GetKey(KeyCode.W))
        {
            movementVector = Vector3.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementVector = Vector3.down;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementVector = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementVector = Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        transform.position += movementVector * maxSpeed * Time.deltaTime;
        */
    }

    private void KillPacStudent()
    {
        GetComponent<Animator>().Play("PacStudentDeath");
        transform.position += movementVector * 0 * Time.deltaTime;
        //isDead = true;
    }
}
