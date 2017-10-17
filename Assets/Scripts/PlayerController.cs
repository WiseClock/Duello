using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    public float movementSpeed;

    public Transform[] groundPoints;
    private float groundRadius = 0.2f;
    public LayerMask groundLayerMask;

    private bool grounded;
    private bool jumping;
    private float jumpTakeOffSpeed = 400.0f;
    private float horizontal=0;

    protected Animator animator;
    private GameObject Player;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
	}

    void Update()
    {
        HandleInput();
        if (rb.velocity.y > 0)
        {
            animator.SetBool("Jump", false);
            animator.SetTrigger("JumpDown");
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        /*else
            animator.SetBool("Attack", false);*/
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", horizontal * horizontal);
        grounded = IsGrounded();
        HandleMovement(horizontal);

        ResetValues();
        
    }

    private void HandleMovement(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        if(horizontal > 0)
        {
            Player.transform.rotation=Quaternion.Euler(0, 125, 0);
        }else if(horizontal < 0)
        {
            Player.transform.rotation = Quaternion.Euler(0, 235, 0);
        }

        //Debug.Log(grounded);
        if(grounded && jumping)
        {
            grounded = false;
            rb.AddForce(new Vector2(0.0f, jumpTakeOffSpeed));
        }
        else if (Input.GetButtonUp("Jump"))
        {
            
            if (rb.velocity.y > 0)
            {
                //animator.SetBool("Jump", false);
                //animator.SetTrigger("JumpDown");
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);   
            }
        }
    }

    private void HandleInput()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            jumping = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Jump", true);
        }
    }

    private bool IsGrounded()
    {
        if(rb.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundLayerMask);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        //animator.SetBool("Jump", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }


    private void ResetValues()
    {
        jumping = false;
    }
}
