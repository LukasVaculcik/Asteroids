using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore")) {
            highScoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
        } else {
            highScoreText.text = "HIGHSCORE: 0";
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
