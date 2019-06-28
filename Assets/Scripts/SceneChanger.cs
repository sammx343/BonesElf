using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {
    public Canvas canvas;
    public string sceneLoader;

    public float changeTimer = 2f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void ChangeScene()
    {
        StartCoroutine(ExecuteAfterTime(changeTimer));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(canvas.GetComponent<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, sceneLoader));
        // Code to execute after the delay
    }
}
