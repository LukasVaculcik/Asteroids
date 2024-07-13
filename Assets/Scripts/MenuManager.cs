using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore")) {
            highScoreText
            .text = 
            "HIGHSCORE: " + 
            PlayerPrefs
            .GetInt("HighScore")
            .ToString();
        } else {
            highScoreText.text = "HIGHSCORE: 0";
        }
    }
}
