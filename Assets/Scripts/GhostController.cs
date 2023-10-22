using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isDead = false;

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
