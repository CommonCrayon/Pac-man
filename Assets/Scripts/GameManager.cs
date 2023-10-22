using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private HudManager hudManager;
    [SerializeField] private PacStudentController PSController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private GhostController[] Ghosts;

    private double Score = 0;
    private int lives = 3;

    public bool GhostKillable = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy the new one to maintain the singleton pattern.
            Destroy(gameObject);
        }
    }

    public void AddScore(double value)
    {
        Score += value;
        hudManager.UpdateScore(Score.ToString());
    }

    public void PowerPelletActivate()
    {
        GhostKillable = true;

        StartCoroutine(GhostAnimator());
    }

    private IEnumerator GhostAnimator()
    {
        audioController.SetBMToGhostScared();
        hudManager.UpdateGhostScaredTimer(10);

        foreach (var ghost in Ghosts)
        {
            ghost.PlayGhostScared();
        }

        yield return new WaitForSeconds(7f);

        foreach (var ghost in Ghosts)
        {
            ghost.PlayGhostRecovering();
        }

        yield return new WaitForSeconds(3f);

        foreach (var ghost in Ghosts)
        {
            ghost.PlayGhostNormal();
        }

        audioController.SetBMToNormal();
        GhostKillable = false;
    }


    public void KillPacStudent()
    {
        lives--;
        PSController.Respawn();
        hudManager.SetLives(lives);
    }
}
