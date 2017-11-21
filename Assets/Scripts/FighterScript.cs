﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour {

    private int damage = 0;
    public Collider[] attacks; //set array size in editor, can hold many attacks colliders.
    private float knockback = 2.5f; //feel free to change value
    private bool damagedFlag;
    private float inAction;
    // Use this for initialization
    void Start () { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inAction = Time.time;
            damagedFlag = false;
        }
        if (damagedFlag == false && Time.time - inAction > 0.15f && Time.time - inAction < 0.6f)
        {
            damage = 10;
            attack(attacks[0]);
        }
    }

    //attack method. Checks for any colliders on the "Hitbox" layer that overlap the selected attacks collider, and sends method calls.
    void attack(Collider col) {
        Collider[] cols = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Hitbox"));
        foreach(Collider c in cols) {
            if (c.transform.root == transform){
                continue;
            }
            c.SendMessageUpwards("TakeDamage", damage);
            Debug.Log("TakeDamage");
            damagedFlag = true;
            c.SendMessageUpwards("TakeKnockback", knockback);
            
        }
    }
}
