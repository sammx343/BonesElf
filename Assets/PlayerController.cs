using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public GameObject Elf;
    private Rigidbody2D rgdb;
    public float maxSpeed = 15;
    public float jumpForce = 15000f;
    public float jumpTakeOffSpeed = 15f;

    public enum PlayerAction { Idle, Run, Crunch, PostCrunch, Jump, Death }
    public PlayerAction playerAction = PlayerAction.Idle;

    private float velocityX = 0;
    private float velocityY = 0;
    
    private bool grounded;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float ForceX = 0;
    public float ForceY = 0;

    public float forcePushY = 5f;
    public float forcePushX = 20f;
    // Use this for initialization
    void Start ()
    {
        rgdb = GetComponent<Rigidbody2D>();
        rgdb.useAutoMass = true;

        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 deltaPosition = rgdb.velocity * Time.deltaTime;
        Vector2 move = Vector2.up * rgdb.position.y;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
       
    }
    
    void Update ()
    {
        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        Vector2 move = Vector2.zero;

        velocityY = rgdb.velocity.y;
        
        ForceX = Mathf.Abs(ForceX) < 1 ? 0 : (ForceX / ForceX) * Mathf.Abs(ForceX - 1);
        ForceY = Mathf.Abs(ForceY) < 1 ? 0 : (ForceY / ForceY) * Mathf.Abs(ForceY - 1);
        
        if(playerAction == PlayerAction.Death)
        {
            if (!grounded)
            {
                if(clipName != "AirDeath" && clipName != "AirDeathFalling" && clipName != "Death" && clipName != "StayDeath")
                    animator.Play("AirDeath");
            }
            else if (grounded)
            {
                if (clipName == "AirDeath" || clipName == "AirDeathFalling")
                {
                    animator.Play("StayDeath");
                }
                else if(clipName != "Death" && clipName != "StayDeath")
                {
                    animator.Play("Death");
                }
            }
        }
        else
        {
            playerActions(clipName);
            move.x = Input.GetAxis("Horizontal");
        }

        rgdb.velocity = new Vector2(move.x * maxSpeed + ForceX, rgdb.velocity.y + ForceY);
        velocityX = Mathf.Abs(rgdb.velocity.x) / maxSpeed;
    }

    private void playerActions(string clipName)
    {
        if (Input.GetKey("right"))
        {
            run("right");
            runAnimation();
        }
        else if (Input.GetKey("left"))
        {
            run("left");
            runAnimation();
        }
        else if (Input.GetKey("down"))
        {
            crunchAnimation();
        }
        else if (Input.GetKeyUp("down"))
        {
            postCrunchAnimation();
        }
        else
        {
            idleAnimation(clipName);
        }

        if (Input.GetKeyDown("up") && grounded)
        {
            rgdb.velocity = new Vector2(velocityX, jumpTakeOffSpeed);

            if (playerAction == PlayerAction.Crunch)
            {
                rgdb.velocity = new Vector2(velocityX, jumpTakeOffSpeed * 1.2f);
            }
        }
        else if (Input.GetKeyUp("up"))
        {
            if (velocityY > 0)
            {
                rgdb.velocity = new Vector2(velocityX, rgdb.velocity.y * 0.5f);
                //velocityY *= 0.5f;
            }
        }

        jumpAnimation();
    }

    private void run(string movement)
    {
        Vector3 theScale = transform.localScale;

        if (movement == "left" && theScale.x > 0f)
        {
            theScale.x *= -1;
            Elf.transform.localScale = theScale;
        }
        else if (movement == "right" && theScale.x < 0f)
        {
            theScale.x *= -1;
            Elf.transform.localScale = theScale;
        }
    }

    private void runAnimation()
    {
        if (velocityX > 0.1f && grounded && velocityY < 0.1f)
        {
            animator.Play("Run");
            playerAction = PlayerAction.Run;
        }
    }

    private void idleAnimation(string clipName)
    {
        if (grounded && velocityX < 0.1f && velocityY < 0.1f && clipName != "PostCrunch")
        {
            animator.Play("Idle");
            playerAction = PlayerAction.Idle;
        }
    }

    private void jumpAnimation()
    {
        if (!grounded && playerAction != PlayerAction.Jump)
        {
            if (rgdb.velocity.y < -0.1f)
            {
                animator.Play("Falling");
                playerAction = PlayerAction.Jump;
            }
            else
            {
                animator.Play("NewJump");
                playerAction = PlayerAction.Jump;
            }
        }
    }

    private void crunchAnimation()
    {
        if (playerAction != PlayerAction.Crunch && grounded)
        {
            animator.Play("PreCrunch");
            playerAction = PlayerAction.Crunch;
        }
    }

    private void postCrunchAnimation()
    {
        if (playerAction == PlayerAction.Crunch && grounded)
        {
            playerAction = PlayerAction.PostCrunch;
            animator.Play("PostCrunch");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerAction != PlayerAction.Death)
        {
            if (collision.gameObject.tag == "Trap")
            {
                var opossingBodyVelocity = collision.GetComponent<Rigidbody2D>().velocity;

                Debug.Log(collision.GetComponent<Rigidbody2D>().velocity);
                playerAction = PlayerAction.Death;

                ForceY = (opossingBodyVelocity.y > 0) ? opossingBodyVelocity.y : velocityY;
                ForceX = (opossingBodyVelocity.x > 0) ? opossingBodyVelocity.x : velocityX;
                
                StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, "SampleScene"));
            }

            if (collision.gameObject.tag == "PushingTotem")
            {
                //transform.Translate(15, 0, 0, Space.World);
                ForceX = 50f;
            }
        }
    }
}
