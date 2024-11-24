using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private Score score;
    void Start()
    {
        scoreText.text = score.score.ToString();
        maxScoreText.text = PlayerPrefs.GetInt("maxScore", 0).ToString();
    }
}
