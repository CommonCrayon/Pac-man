using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewMoving : MonoBehaviour
{
    public int MoveCount = 0;
    private Animator animator;

    private void Start()
    {
         animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (MoveCount < 100)
        {
            transform.position += Vector3.up * 3 * Time.deltaTime;
            animator.Play("Up");
            MoveCount++;
        }
        else if (MoveCount < 200)
        {
            transform.position += Vector3.right * 3 * Time.deltaTime;
            animator.Play("Right");
            MoveCount++;
        }
        else if (MoveCount < 300)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;
            animator.Play("Down");
            MoveCount++;
        }
        else if (MoveCount < 400)
        {
            transform.position += Vector3.left * 3 * Time.deltaTime;
            animator.Play("Left");
            MoveCount++;
        }
        else
        {
            MoveCount = 0;
        }
    }
 }
