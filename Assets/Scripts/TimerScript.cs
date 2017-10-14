using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public GameObject scorePanel;
    public int timer;
    public Text timerText;
    public static bool timerIsActive = true;

	// Use this for initialization
	void Start () {
        StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        if(timer <= 20)
        {
            timerText.color = Color.red;
        }

        timerText.text = timer.ToString();

        // What happens when the timer is stopped
        if(!timerIsActive)
        {
            StopCoroutine("LoseTime");

            if(timer > 0 && timer <= 90)
            {
                // Tell ScoreManager the score
                ScoreManager.timerScore = timer * 100;
                ScoreManager.healthScore = PlayerHealthScript.playerHealth * 200;
            }

            if(timer <= 0 || PlayerHealthScript.playerHealth <=0)
            {
                // Losing End Game Logic Here
                ScoreManager.timerScore = 0;
                ScoreManager.healthScore = 0;
            }

            // Show End Game Panel
            scorePanel.SetActive(true);
        }
	}

    // Reduce the timer by 1 each second passed
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
    }
}
