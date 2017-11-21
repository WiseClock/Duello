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

    public GameObject playerAttacks;
    public GameObject playerColliders;

    public bool isHit = false;

    private float currentPlatformYPos = 0f;
    private bool isFalling = false;
    private int currentLayer = 0;

    const int playerLayer = 8;
    const int platformLayer = 9;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player");
	}

    void Update()
    {
        // if the player is hit freeze all controls
        if (!isHit)
        {
            HandleInput();
            if (rb.velocity.y > 0)
            {
                animator.SetBool("Jump", false);
                animator.SetTrigger("JumpDown");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", horizontal * horizontal);

        // check if player is grounded
        grounded = IsGrounded();

        // once player has fallen below the platform, re-enable collisions with that platform
        if (isFalling && (gameObject.transform.position.y + (GetComponent<BoxCollider2D>().size.y / 2)) < currentPlatformYPos)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
            currentPlatformYPos = 0f;
            isFalling = false;
        }

        // do not allow the player to move while hit
        if (!isHit)
        {
            HandleMovement(horizontal);
        }
        
        ResetValues();
        
    }

    private void HandleMovement(float horizontal)
    {
        // determine movement speed based on horizontal input
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        // check direction and rotate player & weapon hitboxes accordingly
        if(horizontal > 0)
        {
            Player.transform.rotation=Quaternion.Euler(0, 125, 0);
            playerColliders.transform.rotation = Quaternion.Euler(0, 125, 0);
            playerAttacks.transform.rotation = Quaternion.Euler(0, 125, 0);

        }
        else if(horizontal < 0)
        {
            Player.transform.rotation = Quaternion.Euler(0, 235, 0);
            playerColliders.transform.rotation = Quaternion.Euler(0,-55, 0);
            playerAttacks.transform.rotation = Quaternion.Euler(0, -55, 0);
        }

        // if player is on ground and has pressed the jump button add vertical force
        if(grounded && jumping)
        {
            grounded = false;
            rb.AddForce(new Vector2(0.0f, jumpTakeOffSpeed));
        }
        else if (Input.GetButtonUp("Jump")) // if player is in the air and not holding the jump button
        {
            if (rb.velocity.y > 0)
            {
                animator.SetBool("Jump", false);
                animator.SetTrigger("JumpDown");
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);  // divide vertical velocity in half making character fall
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

        // if player presses down on keyboard/joystick, check if on a "fallthrough" platform and disable collision
        if (Input.GetAxis("Vertical") < 0 && currentLayer == platformLayer)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
            currentPlatformYPos = groundPoints[0].transform.position.y;
            isFalling = true;
            grounded = false;
        }
    }

    // checks if groundPoints are colliding with the ground
    // groundPoints are empty game objects located at the players feet
    private bool IsGrounded()
    {
        if(rb.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, groundLayerMask);

                for(int i = 0; i < colliders.Length; i++)
                {
                    
                    if (colliders[i].gameObject != gameObject)
                    {
                        currentLayer = colliders[i].gameObject.layer;
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

    // when the player is hit apply horizontal force in the direction they were attacked from
    // when isHit is true the players controls are frozen
    public IEnumerator GetHit(Vector2 knockback)
    {
        animator.SetTrigger("Hit");
        isHit = true;
        rb.AddForce(knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isHit = false;

    }

}
