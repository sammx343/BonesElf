using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public Canvas canvas;
    public string currentScene;
    public string newScene;

    public float changeTimer = 2f;

    void Start(){
    }

    public void CurrentScene()
    {
        StartCoroutine(CallFadeScene(changeTimer, currentScene));
    }

    public void LoadNewScene()
    {
        StartCoroutine(CallFadeScene(changeTimer, newScene));
    }

    public GameObject CreateBackgroundElementToFade(){
        GameObject sceneFaderBackground = new GameObject("sceneFaderBackground");
        sceneFaderBackground.transform.SetParent(canvas.transform);

        RectTransform rt = sceneFaderBackground.AddComponent<RectTransform>();
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(canvasRt.sizeDelta.x, canvasRt.sizeDelta.y);
        rt.transform.position  = canvasRt.transform.position;

        Image img = sceneFaderBackground.AddComponent<Image>();

        img.color = Color.black;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);

        return sceneFaderBackground;
    }

    IEnumerator CallFadeScene(float time, string newScene)
    {

        float fadeDuration = 3f;
        Fader fader = CreateBackgroundElementToFade().AddComponent<Fader>();
        fader.fadeObjectWithChilds(FadeDirection.In, fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        ChangeScene(newScene);
        // StartCoroutine(canvas.GetComponent<SceneFader>().FadeAndLoadScene(FadeDirection.In, newScene));
    }

    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
