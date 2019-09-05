using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGeneratorController : MonoBehaviour {
    public GameObject trapPrefab;
    public float objectVelocity = 20f;
    public float generatorTimer = 5f;
    private float currentGeneratorTimer = 0f;

    public float animationTimer = 0f;
    public float ANIMATION_TIMER = 1f;
    private bool hasShot = false;
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        currentGeneratorTimer = generatorTimer;
        //StartGenerator();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (currentGeneratorTimer < 0f)
        {
            CreateEnemy();
            currentGeneratorTimer = generatorTimer;
            trapDidShoot();
        }
        else {
            currentGeneratorTimer -= Time.deltaTime;
        }

        if (hasShot)
        {
            if (animationTimer > 0)
            {
                animationTimer -= Time.deltaTime;
            }
            else
            {
                hasShot = false;
                animator.SetBool("hasShot", false);
            }
        }
	}

    void CreateEnemy()
    {
        Vector3 pos = transform.position + new Vector3(0,0,1);
        TrapController trapController = Instantiate(trapPrefab, pos, transform.rotation * Quaternion.Euler(0f, 0f, 45f))
            .GetComponent<TrapController>();

        trapController.velocity = objectVelocity;
    }

    private void trapDidShoot() 
    {
        animator.SetBool("hasShot", true);
        animationTimer = ANIMATION_TIMER;
        hasShot = true;
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }
}
