using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneController : MonoBehaviour {

	// Use this for initialization
	public Canvas canvas;
	void Start () {
		GameObject panelMushroomFade = canvas.transform.Find("PanelMushroomFade").gameObject;

        if(panelMushroomFade != null){
            panelMushroomFade.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
