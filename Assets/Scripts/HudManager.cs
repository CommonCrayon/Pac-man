using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject[] LivesList;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text GameTimerText;
    [SerializeField] private TMP_Text GhostScaredTimerText;
    [SerializeField] private TMP_Text CountdownText;
    [SerializeField] private TMP_Text GameOverText;

    private float gameTimer = 0;
    private float ghostScaredTimer = 0;

    private void Update()
    {
        gameTimer += Time.deltaTime;
        int hours = Mathf.FloorToInt(gameTimer / 3600);
        int minutes = Mathf.FloorToInt((gameTimer % 3600) / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);

        string timerText = string.Format("Time: {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        GameTimerText.text = timerText;
    }

    public IEnumerator Countdown()
    {
        Time.timeScale = 0;

        CountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1);

        CountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1);

        CountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1);

        CountdownText.text = "GO";
        yield return new WaitForSecondsRealtime(1);

        CountdownText.text = "";

        Time.timeScale = 1;
    }

    public void SetLives(int noOfLives)
    {
        if (noOfLives == 3)
        {
            LivesList[0].SetActive(true);
            LivesList[1].SetActive(true);
            LivesList[2].SetActive(true);
        }
        else if (noOfLives == 2)
        {
            LivesList[0].SetActive(true);
            LivesList[1].SetActive(true);
            LivesList[2].SetActive(false);
        }
        else if (noOfLives == 1)
        {
            LivesList[0].SetActive(true);
            LivesList[1].SetActive(false);
            LivesList[2].SetActive(false);
        }
        else
        {
            LivesList[0].SetActive(false);
            LivesList[1].SetActive(false);
            LivesList[2].SetActive(false);
        }
    }

    public void UpdateScore(string value)
    {
        ScoreText.text = $"Score: {value}";
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateGhostScaredTimer(float timerValue)
    {
        ghostScaredTimer = timerValue;
        StartCoroutine(StartGhostScaredTimer());
    }

    private IEnumerator StartGhostScaredTimer()
    {
        while (ghostScaredTimer > 0)
        {
            GhostScaredTimerText.text = $"Ghost Scared Timer: {ghostScaredTimer.ToString("00")}";
            yield return new WaitForSeconds(1f);
            ghostScaredTimer -= 1;
        }

        GhostScaredTimerText.text = "";
    }

    public float GameOver()
    {
        GameOverText.enabled = true;

        return gameTimer;
    }
}
