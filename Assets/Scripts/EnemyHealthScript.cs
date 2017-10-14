using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthScript : MonoBehaviour
{

    public static int enemyHealth;
    public Text enemyHealthText;

    // Use this for initialization
    void Start()
    {
        enemyHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthText.text = "Enemy Health: " + enemyHealth + "%";

        if (enemyHealth <= 20)
        {
            enemyHealthText.text = "Enemy Health: <color=red>" + enemyHealth + "%</color>";
        }

        if (enemyHealth <= 0)
        {
            TimerScript.timerIsActive = false;
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;

    }
}
