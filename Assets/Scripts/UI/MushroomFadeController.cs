using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomFadeController : MonoBehaviour {

	// Use this for initialization
    GameObject panelMushroomFade;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisableMushroomPanel()
    {
        gameObject.SetActive(false);
    }
}
