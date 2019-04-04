using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatformController : MonoBehaviour {
    public float timer = 5f;
    public GameObject gameObject;
    public bool destroyNow = false;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (destroyNow)
        {

        }
	}

    void DestroyPlatform()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestroyablePlatform")
        {
            Debug.Log("Hola");
            Destroy(gameObject);
        }
    }
}
