using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentScore")) {
            currentScoreText.text = "SCORE: " + PlayerPrefs.GetInt("CurrentScore").ToString();   
        }
        if (PlayerPrefs.HasKey("HighScore")) {
            highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore").ToString();   
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
