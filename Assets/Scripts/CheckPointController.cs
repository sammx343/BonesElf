using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

	public SceneEnum scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkPointSaveStatus()
	{
		ElfStatus.SaveSelfGameStatus(scene.ToString(), transform.position);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ( collision.gameObject.tag == "Elf" )
		{
			ElfStatus.checkPointPosition = gameObject.transform.position;
			checkPointSaveStatus();
		}
	}
}
