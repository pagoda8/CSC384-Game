//Class to manage scores, high scores and other user stats

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager shared; //Reference for other classes

    //Text labels on screen
    public Text highScoreText;
    public Text currentScoreText;
    public Text linesClearedText;

    //Variables to count stats
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

    //Add points to current score
    public void AddPoints(int points) {
        currentScore += points;
        currentScoreText.text = "Current Score: " + currentScore.ToString();

        //Update high score if needed
        if (currentScore > highScore) {
            highScore = currentScore;
            highScoreText.text = "High Score: " + highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    //Adds lines cleared
    public void AddLinesCleared(int amount) {
        if (amount > 0) {
            linesCleared += amount;
            linesClearedText.text = "Lines Cleared: " + linesCleared.ToString();
            int total = PlayerPrefs.GetInt("totalLinesCleared", 0);
            PlayerPrefs.SetInt("totalLinesCleared", total + amount);
        }
    }

    //Reset current score and lines cleared
    public void Reset() {
        currentScore = 0;
        linesCleared = 0;
        currentScoreText.text = "Current Score: " + currentScore.ToString();
        linesClearedText.text = "Lines Cleared: " + linesCleared.ToString();
    }
}
