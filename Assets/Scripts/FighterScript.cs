﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour {

    private int damage = 0;
    public Collider[] attacks; //set array size in editor, can hold many attacks colliders.
    private float knockback = 2.5f; //feel free to change value
    
    private float _attackBonusFactor = 0;
    private float _attackBonusEnd = -1;

    // Use this for initialization
    void Start () { }
	
	// Update is called once per frame
	void Update () {
        // bonus end
        if (_attackBonusFactor != 0 && _attackBonusEnd != -1 && _attackBonusEnd < Time.realtimeSinceStartup)
            _attackBonusFactor = 0;

        if (Input.GetButtonDown("Fire1")) {
            damage = Mathf.RoundToInt(10 * (1 + _attackBonusFactor));
            attack(attacks[0]);
        }
	}

    public void SetAttackBuff(object[] arguments)
    {
        float zeroBasedFactor = (float)arguments[0];
        float buffEndTime = (float)arguments[1];
        _attackBonusFactor = zeroBasedFactor;
        _attackBonusEnd = buffEndTime;
    }

    //attack method. Checks for any colliders on the "Hitbox" layer that overlap the selected attacks collider, and sends method calls.
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
