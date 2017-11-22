﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterScript : MonoBehaviour {
    public AudioClip attack1;
    private AudioSource _audioSource;
    private int damage = 0;
    public Collider[] attacks; //set array size in editor, can hold many attacks colliders.

    private float _attackBonusFactor = 0;
    private float _attackBonusEnd = -1;

    private float knockback = 0.75f; //feel free to change value

    private PlayerController pc;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        pc = GetComponent<PlayerController>();
    }
	
	
	// Update is called once per frame
	void Update () {
        // bonus end
        if (_attackBonusFactor != 0 && _attackBonusEnd != -1 && _attackBonusEnd < Time.realtimeSinceStartup)
            _attackBonusFactor = 0;

        if (Input.GetButtonDown("Fire1") && !pc.isHit) {
            damage = Mathf.RoundToInt(10 * (1 + _attackBonusFactor));
             _audioSource.PlayOneShot(attack1);
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
