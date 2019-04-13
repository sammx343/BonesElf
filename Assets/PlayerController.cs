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

    public enum PlayerAction { Idle, Run, Crunch, PostCrunch, Jump }
    public PlayerAction playerAction = PlayerAction.Idle;

    private float velocityX = 0;
    private float velocityY = 0;
    
    private bool grounded;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float ForceX = 0;

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
        move.x = Input.GetAxis("Horizontal");
        
        ForceX = ForceX < 1 ? 0 : (ForceX / ForceX) * Mathf.Abs(ForceX - 2);

        rgdb.velocity = new Vector2(move.x * maxSpeed + ForceX, rgdb.velocity.y);

        velocityX = Mathf.Abs(rgdb.velocity.x) / maxSpeed;

        if (Input.GetKey("right"))
        {
            run("right");
            runAnimation();
        }
        else if(Input.GetKey("left"))
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
        if (velocityX > 0.1f && grounded)
        {
            animator.Play("Run");
            playerAction = PlayerAction.Run;
        }
    }

    private void idleAnimation(string clipName)
    {
        if (grounded && velocityX < 0.1f && clipName != "PostCrunch")
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
        if (playerAction == PlayerAction.Crunch && grounded && velocityX < 0.1f)
        {
            playerAction = PlayerAction.PostCrunch;
            animator.Play("PostCrunch");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.Out, "SampleScene"));
        }

        if (collision.gameObject.tag == "PushingTotem")
        {
            //transform.Translate(15, 0, 0, Space.World);
            Debug.Log("Touching");
            Vector2 impulse = new Vector2(5.0f, 5.0f);
            ForceX = 60f;
            //rgdb.velocity = new Vector2(30.0f, 0);
            //pushObject();
        }

        if (collision.gameObject.tag == "DestroyablePlatform")
        {
            //Debug.Log("DESTROY");
            //Destroy(collision.gameObject);
        }
    }
}
