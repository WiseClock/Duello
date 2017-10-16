using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour {

    private float damage = 0.0f;
    public Collider[] attacks;
    private float knockback = 2.5f;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
            damage = 10f;
            attack(attacks[0]);
        }
	}

    void attack(Collider col) {
        Collider[] cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
        foreach(Collider c in cols) {
            if (c.transform.root == transform){
                continue;
            }
            c.SendMessageUpwards("TakeDamage", damage);
            c.SendMessageUpwards("TakeKnockback", knockback);
            
        }
    }
}
