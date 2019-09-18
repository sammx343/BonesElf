using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishController : MonoBehaviour {

	// Use this for initialization

	SceneChanger sceneChanger;
	void Start () {
		sceneChanger = GetComponent<SceneChanger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Elf")
		{
			sceneChanger.LoadNewScene();
		}
	}
}
