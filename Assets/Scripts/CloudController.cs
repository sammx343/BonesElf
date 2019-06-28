using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {
    public float velocityX;

    public bool isGenerated;
    private Utilities utilities = new Utilities();

    // Use this for initialization
    void Start () {
        Rigidbody2D rgbd = GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(velocityX, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
