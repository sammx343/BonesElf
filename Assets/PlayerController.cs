using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    public GameObject Elf;
    private Rigidbody2D rgdb;
    public float velocity = 15f;
    public float jumpForce = 15000f;

    public enum PlayerAction { Idle, Run, Crunch, Jump, PreJump }
    public PlayerAction playerAction = PlayerAction.Idle;

    public LayerMask groundLayers;

    bool running = false;
    bool isGrounded = false;

    // Use this for initialization
    void Start ()
    {
        rgdb = GetComponent<Rigidbody2D>();
        rgdb.useAutoMass = true;
        rgdb.gravityScale = 2;

        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
            new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayers);
        
        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (Input.GetKey("right"))
        {
            run("right");
        }
        else if (Input.GetKey("left"))
        {
            run("left");
        }
        else if (Input.GetKey("down"))
        {
            if (playerAction != PlayerAction.Crunch)
            {
                animator.Play("PreCrunch");
            }

            playerAction = PlayerAction.Crunch;
        }
        else if (Input.GetKey("up") && isGrounded)
        {
            if (playerAction != PlayerAction.PreJump)
            {
                animator.Play("PreJump");
                playerAction = PlayerAction.PreJump;
            }
        }
        else if (playerAction == PlayerAction.PreJump && clipName == "Jump")
        {
            rgdb.AddForce(Vector2.up* jumpForce);
            playerAction = PlayerAction.Jump;
        }
        else if(playerAction != PlayerAction.Jump && playerAction != PlayerAction.PreJump)
        {
            playerAction = PlayerAction.Idle;
            animator.Play("Idle");
        }
	}

    void OnCollisionStay()
    {
        Debug.Log("tT");
    }

    private void run(string movement)
    {
        playerAction = PlayerAction.Run;
        animator.Play("Run");

        var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        rgdb.transform.position += move * velocity * Time.deltaTime;

        Vector3 theScale = Elf.transform.localScale;

        if (movement == "left" && theScale.x > 0f)
        {
            theScale.x *= -1;
            Elf.transform.localScale = theScale;
        }else if(movement == "right" && theScale.x < 0f)
        {
            theScale.x *= -1;
            Elf.transform.localScale = theScale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(255, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 0.505f), new Vector2(1, 0.01f));
    }
}
