using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager shared; //Reference for other classes

    public Text highScoreText;
    public Text currentScoreText;
    public Text linesClearedText;

    int highScore;
    int currentScore;
    int linesCleared;

    private void Awake() {
        shared = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);

        highScoreText.text = "High Score: " + highScore.ToString();
        currentScoreText.text = "Current Score: " + currentScore.ToString();
        linesClearedText.text = "Lines Cleared: " + linesCleared.ToString();
    }

    public void AddPoints(int points) {
        currentScore += points;
        currentScoreText.text = "Current Score: " + currentScore.ToString();

        if (currentScore > highScore) {
            highScore = currentScore;
            highScoreText.text = "High Score: " + highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void AddLinesCleared(int amount) {
        linesCleared += amount;
        linesClearedText.text = "Lines Cleared: " + linesCleared.ToString();
    }

    public void Reset() {
        currentScore = 0;
        linesCleared = 0;
        currentScoreText.text = "Current Score: " + currentScore.ToString();
        linesClearedText.text = "Lines Cleared: " + linesCleared.ToString();
    }
}
