using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    public ParticleSystem[] torchEffects;
    private int currentScore = 0;
    private int highScore = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentScore")) {
            currentScore = PlayerPrefs.GetInt("CurrentScore");
            currentScoreText.text = "SCORE: " + currentScore.ToString();   
        }
        if (PlayerPrefs.HasKey("HighScore")) {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "HIGHSCORE: " + highScore.ToString();   
        }

        if (currentScore >= highScore) {
            foreach (var effect in torchEffects) {
                effect.Play(true);
            }
        }
    }
}
