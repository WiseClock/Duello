using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int healthScore;
    public static int timerScore;
    public static int totalScore;
    private static string highScoreTag;
    private static string highScoreDateTag;
    private int highScore;
    private static int checkHighScore = 0;
    private static string highScoreDate;
    public Text scoreText;
    public Text highScoreText;

    void Awake()
    {        
        // Reset the scores
        healthScore = 0;
        timerScore = 0;
        totalScore = 0;
    }

    void Update()
    {
        // Set the display text when the game ends
        if (totalScore > 0)
        {
            scoreText.text = string.Format("{0,-20}{1,5}\n{2,-20}{3,7}\n\n{4,-20}{5,7}"
                                        , "HEALTH REMAINING:"
                                        , healthScore
                                        , "TIME REMAINING:"
                                        , timerScore
                                        , "TOTAL SCORE:"
                                        , totalScore);
        } else
        {
            scoreText.text = "YOU LOSE!!\n\nGAME OVER";
        }

        // Display the current high score
        highScore = PlayerPrefs.GetInt("highscore1");
        //highScoreText.text = "CURRENT HIGH SCORE: " + highScore;
        highScoreText.text = string.Format("{0} {1} ({2})"
                                        , "CURRENT HIGH SCORE:"
                                        , highScore
                                        , highScoreDate);
    }

    public static void UpdateScore()
    {
        totalScore = healthScore + timerScore;
        updateCurrentScore(totalScore);
        int currentScore = totalScore;

        // Go through all saved high scores
        for (int i = 1; i <= 5; i++)
        {

            // Get High Score
            highScoreTag = "highscore" + i.ToString();
            checkHighScore = PlayerPrefs.GetInt(highScoreTag, 0);

            // Get High Score Date
            highScoreDateTag = "highscoredate" + i;
            highScoreDate = PlayerPrefs.GetString(highScoreDateTag, "1-Jan-2000 12:00:00 AM");


            // Check for a new high score, set if needed
            if (currentScore > checkHighScore)
            {
                DateTime thisDate = DateTime.Now;
                PlayerPrefs.SetString(highScoreDateTag, thisDate.ToString("d-MMM-yyyy h:mm:ss tt"));
                PlayerPrefs.SetInt(highScoreTag, currentScore);
                //PlayerPrefs.SetString(highScorerTag, tempScorer);
                currentScore = checkHighScore;
            }
        }
    }

    private static void updateCurrentScore(int score)
    {
        int currentScore = PlayerPrefs.GetInt("currentscore", 0) + score;
        PlayerPrefs.SetInt("currentscore", currentScore);
    }
}
