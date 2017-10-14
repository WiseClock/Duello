using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int healthScore;
    public static int timerScore;
    public static int totalScore;
    private int highScore;
    public Text scoreText;
    public Text highScoreText;

    void Awake()
    {
        // Get current high score
        highScore = PlayerPrefs.GetInt("highscore", 0);

        // Reset the scores
        healthScore = 0;
        timerScore = 0;
        totalScore = 0;
    }


    void Update()
    {
        totalScore = healthScore + timerScore;

        // Check for a new high score, set if needed
        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("highscore", totalScore);
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
        highScoreText.text = "CURRENT HIGH SCORE: " + highScore;
        
    }
}
