using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGeneratorController : MonoBehaviour {
    public GameObject trapPrefab;
    public float objectVelocity = 20f;
    public Vector2 movementOrientation = Vector2.up;
    public float generatorTimer = 5f;
    // Use this for initialization
    void Start () {
        StartGenerator();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEnemy()
    {
        TrapController trapController = Instantiate(trapPrefab, transform.position, transform.rotation)
            .GetComponent<TrapController>();

        trapController.velocity = objectVelocity;
        trapController.movementOrientation = movementOrientation;
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }
}
