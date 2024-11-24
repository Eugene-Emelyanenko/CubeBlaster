using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText;
    public int startValue;
    private int currentValue;
    private Score score;
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>().GetComponent<WaveManager>();
        score = FindObjectOfType<Score>().GetComponent<Score>();
        currentValue = startValue;
        valueText.text = currentValue.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            currentValue--;
            valueText.text = currentValue.ToString();
            if (currentValue <= 0)
            {
                waveManager.DeleteObstacle(gameObject);
                score.IncreaseScore(startValue);
                Destroy(gameObject);
            }
        }
    }
}
