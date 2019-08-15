using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneratorController : MonoBehaviour {
    public float generatorTimer = 1;
    public float minTime = 4;
    public float maxTime = 10;
    public float cloudSpeed = -5;
    public float cloudSize = 2;
    public float Zindex;

    public Sprite[] sprites;
    public GameObject cloud;

    private Utilities utilities = new Utilities();
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (generatorTimer > 0)
        {
            generatorTimer -= Time.deltaTime;
        }
        else
        {
            generateCloud();
            generatorTimer = utilities.RandomNumber(minTime, maxTime);
        }
	}

    private void generateCloud()
    {
        cloud.transform.localScale = new Vector3(cloudSize, cloudSize, 1);
        Vector3 cloudPosition = new Vector3(transform.position.x, transform.position.y + utilities.RandomNumber(-1, 3), Zindex);
        var generatedCloud = Instantiate(cloud, cloudPosition, transform.rotation);

        generatedCloud.GetComponent<Rigidbody2D>().velocity = new Vector2(cloudSpeed, 0);

        SpriteRenderer cloudSpriteRenderer = generatedCloud.AddComponent<SpriteRenderer>();
        float rand = utilities.RandomNumber(0, sprites.Length * 100);
        int position = (int)Mathf.Floor( rand/100 );
        cloudSpriteRenderer.sprite = sprites[position];

    }
}
