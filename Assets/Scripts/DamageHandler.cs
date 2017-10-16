using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    
    public GameObject Opponent;
    private Rigidbody2D body;
    private Rigidbody2D oppponent;
	// Use this for initialization
	void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        oppponent = Opponent.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage) {
        Debug.Log("Took damage: " + damage);
    }

    public void TakeKnockback(float knockback) {
        Debug.Log("Body position: " + body.position);
        Debug.Log("Opponent position: " + oppponent.position);
        Vector2 direction = oppponent.position - body.position;
        direction.Normalize();
        ForceMode2D mode = ForceMode2D.Impulse;
        body.AddForce(direction * -knockback, mode);
        Debug.Log(direction);
        
    }
}
