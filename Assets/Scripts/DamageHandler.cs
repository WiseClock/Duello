using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    public Rigidbody2D body;
    public Rigidbody2D oppponent;
	// Use this for initialization
	void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        oppponent = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage) {
        Debug.Log("Took damage: " + damage);
    }

    public void TakeKnockback(float knockback) {
        Vector2 direction = oppponent.position - body.position;
        direction.Normalize();
        ForceMode2D mode = ForceMode2D.Impulse;
        body.AddForce(direction * knockback, mode);
        Debug.Log(direction);
        
    }
}
