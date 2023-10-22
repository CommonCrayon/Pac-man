using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject PacStudent;

    [SerializeField] private HudManager hudManager;
    [SerializeField] private PacStudentController PSController;
    [SerializeField] private AudioController audioController;
    [SerializeField] private GhostController[] Ghosts;


    private double Score = 0;
    private int lives = 3;

    [HideInInspector] public bool GhostKillable = false;

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

        // Stops timescale to do this countdown
        StartCoroutine(hudManager.Countdown());
    }

    private void FixedUpdate()
    {
        // Finish the game if all pellets are collected
        if (GameObject.FindGameObjectWithTag("NormalPellet") == null && GameObject.FindGameObjectWithTag("PowerPellet") == null)
        {
            GameOver();
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

        if (lives == 0)
        {
            GameOver();
        }
        else
        {
            PSController.Respawn();
            hudManager.SetLives(lives);
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        float gameTimer = hudManager.GameOver();

        int hours = Mathf.FloorToInt(gameTimer / 3600);
        int minutes = Mathf.FloorToInt((gameTimer % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);

        string timerText = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        if (Score > double.Parse(PlayerPrefs.GetString("HighScore", "0")))
        {
            PlayerPrefs.SetString("HighScore", Score.ToString());
            PlayerPrefs.SetString("HighTime", timerText.ToString());
        }
        else if (Score == double.Parse(PlayerPrefs.GetString("HighScore", "0")))
        {
            string highTime = PlayerPrefs.GetString("HighTime", "00:00:00");
            if (string.Compare(timerText, highTime) < 0)
            {
                PlayerPrefs.SetString("HighScore", Score.ToString());
                PlayerPrefs.SetString("HighTime", timerText);
            }
        }

        StartCoroutine(GoToStartScene());
    }

    private IEnumerator GoToStartScene()
    {
        yield return new WaitForSecondsRealtime(3);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
