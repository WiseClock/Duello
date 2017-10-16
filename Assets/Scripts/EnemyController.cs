using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D rb;
    private Collider2D collider;
    private float movementSpeed = 7.5f;

    public Transform[] groundPoints;
    private float groundRadius = 0.2f;
    public LayerMask groundLayerMask;
    private bool grounded;

    private float jumpTakeOffSpeed = 400.0f;

    public GameObject player;
    private Collider2D playerCollider;
    private float collisionBuffer = 1.5f;

    bool jumping = false;
    bool readyToJump = true;

    public GameObject navMesh;
    private NavigationScript navScript;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();
        navScript = navMesh.GetComponent<NavigationScript>();
    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();

        if (navScript.targetNode != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if(distance > collisionBuffer)
            {
                MoveToTargetNode();
            }
            else
            {
                StopMoving();
                Debug.Log("Attacking");
            }

        }

        if (grounded && !readyToJump)
        {
            readyToJump = true;
        }
    }

    private void MoveToTargetNode()
    {
        if(!(navScript.currentEnemyNode.GetComponent<NavNodeScript>().isGapEdge && navScript.targetNode.GetComponent<NavNodeScript>().isGapEdge))
        {
            if (transform.position.x > navScript.targetNode.transform.position.x)
            {
                MoveLeft();
            }
            else if (transform.position.x < navScript.targetNode.transform.position.x)
            {
                MoveRight();
            }
        }
        else 
        {
            if (readyToJump)
            {
                if (navScript.targetNodeDirection < 0)
                {
                    JumpLeft();
                }
                else
                {
                    JumpRight();
                }
            }
        }
    }

    private void SeekPlayer()
    {
        if(transform.position.x - (collider.bounds.size.x / 2) > player.transform.position.x + (playerCollider.bounds.size.x / 2) + collisionBuffer)
        {
            MoveLeft();
        }
        else if (transform.position.x + (collider.bounds.size.x / 2) < player.transform.position.x + (playerCollider.bounds.size.x / 2) - collisionBuffer)
        {
            MoveRight();
        }
        else
        {
            StopMoving();
        }
    }

    private void HandleMovement(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        //MoveLeft(true);

        
    }

    private void HandleInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            //jumping = true;
        }
    }

    public bool IsGrounded()
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

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-1.0f * movementSpeed, rb.velocity.y);
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(1.0f * movementSpeed, rb.velocity.y);
    }

    private void StopMoving()
    {
        rb.velocity = new Vector2(0.0f, rb.velocity.y);
    }

    private void HighJump()
    {
        if(grounded)
        {
            grounded = false;
            jumping = false;
            rb.AddForce(new Vector2(0.0f, 400.0f));
        }

    }

    private void ShortJump()
    {
        if (grounded)
        {
            grounded = false;
            jumping = false;
            rb.AddForce(new Vector2(0.0f, 350.0f));
        }

    }

    private void JumpLeft()
    {
        if (grounded)
        {
            StopMoving();
            readyToJump = false;
            rb.velocity = new Vector2(-1.0f * movementSpeed, rb.velocity.y);
            rb.AddForce(new Vector2(0.0f, 150.0f));
        }
    }
    private void JumpRight()
    {
        if (grounded)
        {
            StopMoving();
            readyToJump = false;
            rb.velocity = new Vector2(1.0f * movementSpeed, rb.velocity.y);
            rb.AddForce(new Vector2(0.0f, 150.0f));
        }
    }


}
