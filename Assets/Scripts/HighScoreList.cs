using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreList : MonoBehaviour {

    private int[] highScores = { 0, 0, 0, 0, 0 };
    public Text highScoreText;

	// Use this for initialization
	void Start () {
		for(int i = 1; i <= 5; i++)
        {
            string highScore = "highscore" + i.ToString();
            highScores[i-1] = PlayerPrefs.GetInt(highScore, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        highScoreText.text = "CURRENT HIGH SCORES\n"
                              + "\n" + highScores[0]
                              + "\n" + highScores[1]
                              + "\n" + highScores[2]
                              + "\n" + highScores[3]
                              + "\n" + highScores[4];
    }
}
