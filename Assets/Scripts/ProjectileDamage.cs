using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

    private float knockback = 0.75f; //feel free to change value
    
    private int damage = 15;

    public bool isPlayer = true;

    public GameObject particle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy") && isPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.collider.SendMessageUpwards("TakeDamage", damage);
            collision.collider.SendMessageUpwards("TakeKnockback", knockback);
        }
        else if (collision.collider.CompareTag("Player") && !isPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.collider.SendMessageUpwards("TakeDamage", damage);
            collision.collider.SendMessageUpwards("TakeKnockback", knockback);
        }

        Destroy(gameObject);

        if(particle != null)
        {
            Destroy(particle);
        }
    }
}
