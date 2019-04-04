using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 15;
    public float jumpTakeOffSpeed = 20;

    public GameObject Elf;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float velocityX = 0;

    public enum PlayerAction { Idle, Run, Crunch, PostCrunch, Jump }
    public PlayerAction playerAction = PlayerAction.Idle;

    private bool pushed = false;
    private float pushTime;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        targetVelocity = move * maxSpeed;

        if (pushed)
        {
            if(pushTime + 1 > Time.time)
            {
                rb2d.velocity = new Vector2(30.0f, 0);
            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
                pushed = false;
            }
        }

        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        velocityX = Mathf.Abs(velocity.x) / maxSpeed;

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
            velocity.y = jumpTakeOffSpeed;

            if (playerAction == PlayerAction.Crunch)
            {
                velocity.y = jumpTakeOffSpeed * 1.2f;
            }
        }
        else if (Input.GetKeyUp("up"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        jumpAnimation();

        animator.SetBool("grounded", grounded);
        
    }

    private void crunchAnimation()
    {
        if (playerAction != PlayerAction.Crunch && grounded)
        {
            animator.Play("PreCrunch");
            playerAction = PlayerAction.Crunch;
        }
    }

    private void idleAnimation(string clipName)
    {
        if(grounded && velocityX < 0.1f && clipName != "PostCrunch")
        {
            animator.Play("Idle");
            playerAction = PlayerAction.Idle;
        }
    }

    private void postCrunchAnimation()
    {
        if(playerAction == PlayerAction.Crunch && grounded && velocityX < 0.1f)
        {
            playerAction = PlayerAction.PostCrunch;
            animator.Play("PostCrunch");
        }
    }

    private void runAnimation()
    {
        if(velocityX > 0.1f && grounded)
        {
            animator.Play("Run");
            playerAction = PlayerAction.Run;
        }
    }

    private void jumpAnimation()
    {
        if(!grounded && playerAction != PlayerAction.Jump)
        {
            if (velocity.y < -0.1f)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, "SampleScene"));
        }

        if(collision.gameObject.tag == "PushingTotem")
        {
            //transform.Translate(15, 0, 0, Space.World);
            pushed = true;
            pushTime = Time.time;
            pushObject();
        }

        if(collision.gameObject.tag == "DestroyablePlatform")
        {
            Debug.Log("DESTROY");
            Destroy(collision.gameObject);
        }
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
}