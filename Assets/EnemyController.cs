using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float limitX1;
    public float limitX2;
    public GameObject body;

    private Rigidbody2D rgdb;
    private Vector2 normalPosition;
    private Vector2 setVelocity;
    Animator animator;
    public float ATTACKING_TIME = 5;
    private float attackinTimer;

    public enum EnemyAction { Walking, Scared, Angry};
    public EnemyAction enemyAction = EnemyAction.Walking;

    // Use this for initialization
    void Start ()
    {
        rgdb = body.GetComponent<Rigidbody2D>();
        normalPosition = rgdb.position;
        setVelocity = new Vector2(-4, rgdb.velocity.y);
        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyAction == EnemyAction.Walking)
        {
            walking();
        }

        if(enemyAction == EnemyAction.Angry)
        {
            Angry();
        }
    }

    private void walking()
    {
        if (rgdb.position.x < normalPosition.x - limitX1)
        {
            setVelocity = new Vector2(4, rgdb.velocity.y);
            transformLocalScale(-1);
        }

        if (rgdb.position.x > normalPosition.x + limitX2)
        {
            setVelocity = new Vector2(-4, rgdb.velocity.y);
            transformLocalScale(1);
        }

        rgdb.velocity = setVelocity;
    }

    private void Angry()
    {
        Debug.Log(attackinTimer);
        if (attackinTimer <= 0)
        {
            Debug.Log("Stops");
            animator.SetBool("Scared", false);
        }
        else
        {
            attackinTimer -= Time.deltaTime;
         }
    }

    public void transformLocalScale(int sign)
    {
        Vector3 theScale = transform.localScale;

        theScale.x = Mathf.Abs(theScale.x) * sign;
        transform.localScale = theScale;
    }

    public void gotScared()
    {
        if (enemyAction == EnemyAction.Walking)
        {
        enemyAction = EnemyAction.Scared;
        animator.SetBool("Scared", true);

        rgdb.velocity = new Vector2(0, 0);
        }
    }

    public void Attacking()
    {
        rgdb.velocity = new Vector2(12 * -transform.localScale.x, rgdb.velocity.y);
        enemyAction = EnemyAction.Angry;
        attackinTimer = ATTACKING_TIME;
    }

    public void Stopping()
    {
        rgdb.velocity = new Vector2(3 * -transform.localScale.x, rgdb.velocity.y);
        enemyAction = EnemyAction.Walking;
    }
}
