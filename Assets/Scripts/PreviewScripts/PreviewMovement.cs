using UnityEngine;

/// <summary>
/// Preview the Movement of Ghosts/Cats
/// </summary>
public class PreviewMoving : MonoBehaviour
{
    public bool CatScared = false;
    public bool CatDead = false;
    public bool CatRecovering = false;
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

            if (CatScared) animator.Play("CatScared");
            else if (CatDead) animator.Play("CatDead");
            else if (CatRecovering) animator.Play("CatRecovering");
            else animator.Play("Up");

            MoveCount++;
        }
        else if (MoveCount < 200)
        {
            transform.position += Vector3.right * 3 * Time.deltaTime;

            if (CatScared) animator.Play("CatScared");
            else if (CatDead) animator.Play("CatDead");
            else if (CatRecovering) animator.Play("CatRecovering");
            else animator.Play("Right");

            MoveCount++;
        }
        else if (MoveCount < 300)
        {
            transform.position += Vector3.down * 3 * Time.deltaTime;

            if (CatScared) animator.Play("CatScared");
            else if (CatDead) animator.Play("CatDead");
            else if (CatRecovering) animator.Play("CatRecovering");
            else animator.Play("Down");

            MoveCount++;
        }
        else if (MoveCount < 400)
        {
            transform.position += Vector3.left * 3 * Time.deltaTime;

            if (CatScared) animator.Play("CatScared");
            else if (CatDead) animator.Play("CatDead");
            else if (CatRecovering) animator.Play("CatRecovering");
            else animator.Play("Left");

            MoveCount++;
        }
        else
        {
            MoveCount = 0;
        }
    }
}
