﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour {
    public GameObject trap;
    public float velocity = 0f;
    public float destroyableTimer = 10f;
    public Vector2 movementOrientation = Vector2.up;
    public bool isDestroyable = false;
    private Rigidbody2D rgdb;

    // Use this for initialization
    void Start () {
        rgdb = GetComponent<Rigidbody2D>();
        // Debug.Log("Right");
        // Debug.Log(transform.right);
        // Debug.Log("Up");
        // Debug.Log(transform.up);
        //rgdb.velocity = newVelocity;
        rgdb.velocity = transform.up * velocity;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDestroyable)
        {
            destroyableTimer -= Time.deltaTime;
            if (destroyableTimer <= 0)
            {
                Destroy(trap);
            }
        }
	}
}
