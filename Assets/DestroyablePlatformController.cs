using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatformController : MonoBehaviour {
    public GameObject platform;
    public GameObject plaftormSpriteObject;
    private SpriteRenderer sprite;
    private bool destroyNow = false;

    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 3f;
    private float startTime;
    
    // Use this for initialization
    void Start () {
        sprite = plaftormSpriteObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (destroyNow)
        {
            float t = (Time.time - startTime) / duration;
            Debug.Log(t);
            Debug.Log(Mathf.SmoothStep(minimum, maximum, t));
            sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, t));
        }
	}

    void DestroyPlatform()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Elf" && !destroyNow)
        {
            destroyNow = true;
            startTime = Time.time;
            StartCoroutine(Example());
        }
    }
    

    IEnumerator Example()
    {
        yield return new WaitForSeconds(duration);
        Destroy(platform);
    }
}
