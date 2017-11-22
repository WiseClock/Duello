using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour {

    
    public GameObject Opponent;
    private Rigidbody2D body;
    private Rigidbody2D oppponent;
    private int health = 100;

    private EnemyScript es;
    private AudioSource _audioSource;
    public AudioClip damageSound;


    private bool isHit = false, Hitted=false;
    public Renderer bodyRend;
    private float damageHitTime, newTime;

    // Use this for initialization
    void Start () {
        body = gameObject.GetComponent<Rigidbody2D>();
        oppponent = Opponent.GetComponent<Rigidbody2D>();

        es = GetComponent<EnemyScript>();
        _audioSource = GetComponent<AudioSource>();

        //bodyRend = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        if (isHit == true)
        {
            damageHitTime = Time.time;
            isHit = false;
            Hitted = true;
        }
        if (Hitted == true) { 
            newTime = Time.time - damageHitTime;
            Debug.Log("Time:" + damageHitTime);
            Debug.Log("newTime:" + newTime);
           /* if (newTime < 0.1f || (newTime > 0.3f && newTime < 0.5f))
            {
                bodyRend.material.SetColor("_Color", Color.red); //.material.SetColor("_Color", Color.red);
            }
            else
            {
                bodyRend.material.SetColor("_Color", Color.cyan);
                if (newTime > 0.7f)
                {
                    isHit = false;
                    Hitted = false;
                }
            }*/
        }
    }


    //public access for the health variable
    public int getHealth() { return health; }

    //Health updater from an attack.
    public void TakeDamage(int damage) {
        health -= damage;

        _audioSource.PlayOneShot(damageSound);

        isHit = true;

        if (health < 0) {
            health = 0;
            TimerScript.timerIsActive = false;
        }
    }

    //Kockback effect upon getting hit.
    public void TakeKnockback(float knockback) {
        //Debug.Log("Body position: " + body.position);
        //Debug.Log("Opponent position: " + oppponent.position);
        Vector2 direction = oppponent.position - body.position;
        direction.Normalize();

        //ForceMode2D mode = ForceMode2D.Impulse;
        //body.AddForce(direction * -knockback, mode);
        //body.MovePosition((body.position + (direction * -knockback)) * Time.deltaTime);
        es.StartCoroutine(es.GetHit(direction * -knockback));
        //body.AddForce(direction * -knockback, mode);

        //ForceMode2D mode = ForceMode2D.Impulse;
        //body.AddForce(direction * -knockback, mode);

    }
}
