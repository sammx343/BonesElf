using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPartController : MonoBehaviour {

    public GameObject fallingPart;
    private Animator animator;

    public float COUNTER_TIME = 2;
    private float counter;
    private bool falls = false;
	// Use this for initialization
	void Start () {
		animator = fallingPart.GetComponent<Animator>();
        counter = COUNTER_TIME;
    }
	
	// Update is called once per frame
	void Update () {
        if (falls)
        {
            counter -= Time.deltaTime;
        }

        if (counter < 0)
        {
            falls = false;
            animator.SetBool("Falls", false);
            counter = COUNTER_TIME;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ElfTag(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ElfTag(collision);
    }

    private void ElfTag(Collider2D collision)
    {
        if (collision.gameObject.tag == "Elf")
        {
            falls = true;
            animator.SetBool("Falls", true);
        }
    }
}
