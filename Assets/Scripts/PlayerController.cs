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
    private float attackColdDown;
    private bool attackFreeze;

    protected Animator animator;
    private GameObject Player;

    public GameObject playerAttacks;
    public GameObject playerColliders;
    private float highter;
    private bool isOnAir;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        attackFreeze = false;
	}

    void Update()
    {
        HandleInput();
        if (grounded)
        {
            highter = Player.transform.position.y;
            Debug.Log("High get");
        }
        else if (grounded == false && Player.transform.position.y - highter > 0.4f)
        {
            isOnAir = true;
        }
        if (Player.transform.position.y - highter < 0.3f && isOnAir == true)
        {
            animator.SetBool("Jump", false);
            Debug.Log("Hittt");
            isOnAir = false;
        }
        /*if (rb.velocity.y > 0)
        {
            //animator.SetBool("Jump", false);
            //animator.SetTrigger("JumpDown");
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
            Player.transform.rotation=Quaternion.Euler(0, 120, 0);
            playerColliders.transform.rotation = Quaternion.Euler(0, 125, 0);
            //playerAttacks.transform.rotation = Quaternion.Euler(0, 125, 0);

        }
        else if(horizontal < 0)
        {
            //Player.transform.rotation = Quaternion.Euler(0, 235, 0);
            Player.transform.rotation = Quaternion.Euler(0, 235, 0);
            playerColliders.transform.rotation = Quaternion.Euler(0,-55, 0);
            //playerAttacks.transform.rotation = Quaternion.Euler(0, -55, 0);
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

        if (Input.GetButtonDown("Fire1") && attackFreeze==false)
        {
            attackColdDown = Time.time;
            animator.SetTrigger("Attack");
            attackFreeze = true;
            Debug.Log("attack on");
        }
        if (Time.time - attackColdDown >= 0.6f && attackFreeze==true)
        {
            attackFreeze = false;
            Debug.Log("attack freeze");
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
