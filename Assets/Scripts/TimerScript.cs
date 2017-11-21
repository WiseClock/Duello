using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public GameObject scorePanel;
    public GameObject Player;
    public GameObject Enemy;
    public int timer;
    public Text timerText;
    public Text playerHealthText;
    public Text enemyHealthText;
    public static bool timerIsActive = true;
    private DamageHandler player;
    private EnemyDamageHandler enemy;

	// Use this for initialization
	void Start () {
        timer = 90;
        player = Player.GetComponent<DamageHandler>();
        enemy = Enemy.GetComponent<EnemyDamageHandler>();
        StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        if(timer <= 20)
        {
            timerText.color = Color.red;
        }

        timerText.text = timer.ToString();

        //Health text updates;
        updateHealthDisplays();
        // What happens when the timer is stopped
        if(!timerIsActive)
        {
            StopCoroutine("LoseTime");

            if(timer > 0 && timer <= 90)
            {
                // Tell ScoreManager the score
                ScoreManager.timerScore = timer * 100;
                ScoreManager.healthScore = player.getHealth() * 200;
            }

            if(timer <= 0 || player.getHealth() <= 0)
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

    //sub method that sorts out the health display logic
    void updateHealthDisplays() {
        //Player health
        playerHealthText.text = "Player Health: " + player.getHealth() + "%";
        if (player.getHealth() <= 20)
        {
            playerHealthText.text = "Player Health: <color=red>" + player.getHealth() + "%</color>";
        }

        //enemy health
        enemyHealthText.text = "Enemy Health: " + enemy.getHealth() + "%";
        if (enemy.getHealth() <= 20)
        {
            enemyHealthText.text = "Enemy Health: <color=red>" + enemy.getHealth() + "%</color>";
        }
    }
}
