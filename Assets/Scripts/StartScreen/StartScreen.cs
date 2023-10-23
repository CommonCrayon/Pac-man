using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text highTimeText;

    private void Awake()
    {
        highScoreText.text = $"High Score: {PlayerPrefs.GetString("HighScore", "0")}";
        highTimeText.text = $"Time: {PlayerPrefs.GetString("HighTime", "00:00:00")}";
    }

    public void OnLevel1ButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLevel2ButtonClick()
    {
        SceneManager.LoadScene(2);
    }
}
