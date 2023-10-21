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

}
