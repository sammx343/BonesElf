using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public GameObject Elf;
    private Rigidbody2D rgdb;
    public float maxSpeed = 15;
    public float ropeSpeed = 2f;
    public float jumpTakeOffSpeed = 15f;

    public enum PlayerAction { Idle, Run, Crunch, PostCrunch, Jump, JumpHigher, Falling, Death, HangUp, ClimbingUp }
    public PlayerAction playerAction = PlayerAction.Idle;

    private float velocityX = 0;
    private float velocityY = 0;
    
    private bool grounded;
    public Transform groundCheck;
    float groundRadius = 0.5f;

    public LayerMask whatIsGround;
    public LayerMask whatIsSlope;

    public float ForceX = 0;
    public float ForceY = 0;

    public float windFriction = 0.20f;
    public float pushingTotemForce = 50f;
    public bool canDie = true;
    Vector2 move;
    private float gravityScale;
    private bool ropeEnd = false;
    private bool HangUpWall = false;
    private float HANGED_ACTIONS_DELAY = 0.5f;
    private float hangedActionsTimer = 0;

    public float CLIMB_VELOCITY_Y = 10;
    public float ClimbingVelocityX = 0;
    public float ClimbingVelocityY = 0;

    public float energy = 100;

    public string sceneName;

    // Use this for initialization
    void Start ()
    {
        rgdb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rgdb.gravityScale;
        //ClimbingVelocityY = CLIMB_VELOCITY_Y;
    }

    private void FixedUpdate()
    {
        ForceX = Mathf.Abs(ForceX) < 4 ? 0 : Mathf.Sign(ForceX) * (Mathf.Abs(ForceX) - gravityScale * windFriction/2);
        ForceY = Mathf.Abs(ForceY) < 4 ? 0 : Mathf.Sign(ForceY) * (Mathf.Abs(ForceY) - gravityScale * windFriction);
        grounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f , 1.5f), CapsuleDirection2D.Vertical, 0, whatIsGround);

        bool IsSlope = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 1.5f), CapsuleDirection2D.Vertical, 0, whatIsSlope);

        animator.SetBool("IsSlope", IsSlope);

        if (playerAction == PlayerAction.ClimbingUp)
        {
            rgdb.velocity = new Vector2(ClimbingVelocityX, ClimbingVelocityY);
        }
        else if (playerAction == PlayerAction.HangUp)
        {
            rgdb.velocity = new Vector2(ForceX, move.y * ropeSpeed);
        }

        else
        {
            rgdb.velocity = new Vector2(move.x * maxSpeed + ForceX, rgdb.velocity.y + ForceY);
        }
    }

    void Update()
    {
        move = Vector2.zero;
        //Debug.Log(rgdb.velocity);
        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        velocityY = rgdb.velocity.y;
        
        if (playerAction == PlayerAction.Death)
        {
            rgdb.gravityScale = gravityScale;
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
            if(playerAction != PlayerAction.ClimbingUp)
            {
                if (playerAction == PlayerAction.HangUp)
                {
                    hangedUpActions(clipName);
                }
                else
                {
                    //Normal set of actions for the player, when no climbing, no death, etc...
                    playerActions(clipName);
                    move.x = Input.GetAxis("Horizontal");
                }
            }
        }

        velocityX = Mathf.Abs(rgdb.velocity.x) / maxSpeed;

        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityY", velocityY);
        animator.SetBool("grounded", grounded);
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
                playerAction = PlayerAction.JumpHigher;
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

        jumpAnimation(clipName);
    }

    private void run(string movement)
    {
        Vector3 theScale = transform.localScale;

        if (movement == "left" && theScale.x > 0f)
        {
            transformLocalScale();
        }
        else if (movement == "right" && theScale.x < 0f)
        {
            transformLocalScale();
        }
    }

    private void runAnimation()
    {
        if (velocityX > 0.1f && grounded)
        {
            //animator.Play("Run");
            playerAction = PlayerAction.Run;
        }
    }

    private void idleAnimation(string clipName)
    {
        if (grounded && velocityX < 0.1f && velocityY < 0.1f && clipName != "PostCrunch")
        {
            //animator.Play("Idle");
            playerAction = PlayerAction.Idle;
        }

    }

    private void jumpAnimation(string clipName)
    {
        if (!grounded && playerAction != PlayerAction.Jump && clipName != "RopeFalling" && playerAction != PlayerAction.JumpHigher)
        {
            if (rgdb.velocity.y > 0.1f && clipName != "Falling" && clipName != "AlwaysFall")
            {
                animator.Play("NewJump");
                playerAction = PlayerAction.Jump;
            }
            else
            {
                animator.Play("Falling");
                playerAction = PlayerAction.Jump;
            }
        }
    }

    private void crunchAnimation()
    {
        if (playerAction != PlayerAction.Crunch && grounded && playerAction != PlayerAction.JumpHigher)
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

    private void hangedUpActions(string clipName)
    {
        move.y = Input.GetAxis("Vertical");

        float horizontalDirection = transform.localScale.x;

        if (hangedActionsTimer <= 0)
        {
            if ( (Input.GetKeyUp("left") && horizontalDirection < 0 || Input.GetKeyUp("right") && horizontalDirection > 0) 
                &&  !HangUpWall)
            {
                animator.Play("RopeChange");
                hangedActionsTimer = HANGED_ACTIONS_DELAY;
                clipName = "RopeChange";
            }
        }
        else
        {
            hangedActionsTimer -= Time.deltaTime;
        }

        if (Input.GetKeyUp("right") && horizontalDirection < 0 || Input.GetKeyUp("left") && horizontalDirection > 0)
        {
            animator.Play("RopeFalling");
            clipName = "RopeFalling";
        }

        if (ropeEnd == true && move.y > 0)
            move.y = 0;

        if (Mathf.Abs(rgdb.velocity.y) > 0.01f)
        {
            animator.Play("Hanging");
        }
        else if(clipName != "RopeFalling" && clipName != "RopeChange")
        {
            animator.Play("HangingIdle");
        }
    }

    public void applyRopeFallingForce()
    {
        ForceX = 50 * -transform.localScale.x;
    }

    public void applyRopeChangeForce()
    {
        ForceX = 29 * transform.localScale.x;
    }

    public void ropeChangeAnimation()
    {
        applyRopeChangeForce();
        transformLocalScale();
    }

    public void transformLocalScale()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        Elf.transform.localScale = theScale;
    }

    public void climbingUp() {
        ClimbingVelocityY = CLIMB_VELOCITY_Y;
    }

    public void climbingUpRight()
    {
        ClimbingVelocityY = 2;
    }

    public void climbUpStart()
    {
        ClimbingVelocityX = 6 * Mathf.Sign(transform.localScale.x);
        playerAction = PlayerAction.ClimbingUp;
        rgdb.gravityScale = 0;
        animator.Play("ClimbUp");
        ropeEnd = true;
    }

    public void setNeutralState()
    {
        rgdb.velocity = Vector2.zero;
        playerAction = PlayerAction.Idle;
        rgdb.gravityScale = gravityScale;
        ropeEnd = false;
    }

    private void ropeHangingActions()
    {
        if (playerAction != PlayerAction.ClimbingUp)
        {
            rgdb.velocity = new Vector2(0, 0);
            rgdb.gravityScale = 0;
            playerAction = PlayerAction.HangUp;
            grounded = false;
            hangedActionsTimer = HANGED_ACTIONS_DELAY;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerAction != PlayerAction.Death)
        {
            if (collision.gameObject.tag == "Trap" || collision.gameObject.tag == "KillPlace")
            {
                if (canDie)
                {
                    //StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, sceneName));
                    FindObjectOfType<SceneChanger>().ChangeScene();
                    playerAction = PlayerAction.Death;
                }

                Vector2 opossingBody = Vector2.zero;
                if (collision.GetComponent<Rigidbody2D>() != null)
                {
                    opossingBody = collision.GetComponent<Rigidbody2D>().velocity;
                }

                if (!grounded)
                {
                    ForceY = opossingBody.y == 0 ? HelperMaxCollisionVelocity(rgdb.velocity.y * -0.7f) : HelperMaxCollisionVelocity(opossingBody.y);
                }
                ForceX = opossingBody.x == 0 ? HelperMaxCollisionVelocity(rgdb.velocity.x * -1f) : HelperMaxCollisionVelocity(opossingBody.x);
            }

            if (collision.gameObject.tag == "Rope")
            {
                ropeHangingActions();
            }

            if (collision.gameObject.tag == "ClimbWall")
            {
                ropeHangingActions();
                HangUpWall = true;
            }

            if (collision.gameObject.tag == "PushingTotem")
            {
                //transform.Translate(15, 0, 0, Space.World);
                ForceX = pushingTotemForce * Mathf.Sign(collision.gameObject.transform.lossyScale.x);
            }

            if (collision.gameObject.tag == "RopeEnd")
            {
                ropeEnd = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RopeEnd")
        {
            ropeEnd = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope" || collision.gameObject.tag == "ClimbWall")
        {
            HangUpWall = false;
            if (playerAction != PlayerAction.Death && playerAction != PlayerAction.ClimbingUp)
            {
                rgdb.gravityScale = gravityScale;
                playerAction = PlayerAction.Falling;
            }
        }

        if (collision.gameObject.tag == "RopeEnd")
        {
            ropeEnd = false;
        }
    }

    private float HelperMaxCollisionVelocity(float velocity)
    {
        return Mathf.Sign(velocity) * Mathf.Min(Mathf.Abs(velocity), 25);
    }
}
