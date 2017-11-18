using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int healthScore;
    public static int timerScore;
    public static int totalScore;
    private string highScoreTag;
    //private string highScorerTag;
    private int highScore;
    private int checkHighScore;
    //private string highScorer;
    public Text scoreText;
    public Text highScoreText;

    void Awake()
    {
        // Get current high score
        //highScore = PlayerPrefs.GetInt("highscore1", 0);
        //highScorer = PlayerPrefs.GetString("highscorer1", "AAA");

        /*PlayerPrefs.SetInt("highscore1", 0);
        PlayerPrefs.SetInt("highscore2", 0);
        PlayerPrefs.SetInt("highscore3", 0);
        PlayerPrefs.SetInt("highscore4", 0);
        PlayerPrefs.SetInt("highscore5", 0);*/

        // Reset the scores
        healthScore = 0;
        timerScore = 0;
        totalScore = 0;
    }


    void Update()
    {
        totalScore = healthScore + timerScore;
        int currentScore = totalScore;

        // Go through all saved high scores
        for (int i = 1; i <= 5; i++)
        {
            // Get High Score
            highScoreTag = "highscore" + i.ToString();
            checkHighScore = PlayerPrefs.GetInt(highScoreTag, 0);

            // Get High Scorer Initials
            //highScorerTag = "highscorer" + i;
            //highScorer = PlayerPrefs.GetString(highScorerTag);

            // Check for a new high score, set if needed
            if (currentScore > checkHighScore)
            {
                PlayerPrefs.SetInt(highScoreTag, currentScore);
                //PlayerPrefs.SetString(highScorerTag, tempScorer);
                currentScore = checkHighScore;
            }
        }
        
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
        highScoreText.text = "CURRENT HIGH SCORE: " + highScore;
        /*highScoreText.text = string.Format("{0} {1} {2,10}"
                                        , "CURRENT HIGH SCORE:"
                                        , highScorer
                                        , highScore);*/
    }
}
