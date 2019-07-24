using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {
    public Canvas canvas;
    public string sceneLoader;
    public string currentScene;
    public string NextScene;

    public float changeTimer = 2f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void CurrentScene()
    {
        StartCoroutine(ExecuteAfterTime(changeTimer, currentScene));
    }

    public void NextLevelScene()
    {
        StartCoroutine(ExecuteAfterTime(changeTimer, NextScene));
    }

    public void ChangeScene()
    {
        StartCoroutine(ExecuteAfterTime(changeTimer, sceneLoader));
    }

    IEnumerator ExecuteAfterTime(float time, string newScene)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(canvas.GetComponent<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, newScene));
        // Code to execute after the delay
    }
}
