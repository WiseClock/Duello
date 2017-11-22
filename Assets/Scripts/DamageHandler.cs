using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    
    public GameObject Opponent;
    private Rigidbody2D body;
    private Rigidbody2D oppponent;
    private int health = 100;
	// Use this for initialization
	void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        oppponent = Opponent.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public access for the health variable
    public int getHealth() { return health; }

    public void RestoreHealth(int amount)
    {
        int healthAfter = health + amount;
        if (healthAfter > 100) healthAfter = 100;
        health = healthAfter;
    }

    //Health updater from an attack.
    public void TakeDamage(int damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
            TimerScript.timerIsActive = false;
        }
    }

    //Kockback effect upon getting hit.
    public void TakeKnockback(float knockback) {
        Debug.Log("Body position: " + body.position);
        Debug.Log("Opponent position: " + oppponent.position);
        Vector2 direction = oppponent.position - body.position;
        direction.Normalize();
        ForceMode2D mode = ForceMode2D.Impulse;
        body.AddForce(direction * -knockback, mode);
    }
}
