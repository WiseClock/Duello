using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour {

    public static int playerHealth;
    public Text playerHealthText;

	// Use this for initialization
	void Start () {
        playerHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
        playerHealthText.text = "Player Health: " + playerHealth + "%";

        if (playerHealth <= 20)
        {
            playerHealthText.text = "Player Health: <color=red>" + playerHealth + "%</color>";
        }

        if (playerHealth <= 0)
        {
            TimerScript.timerIsActive = false;
        }
	}

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;

    }
}
