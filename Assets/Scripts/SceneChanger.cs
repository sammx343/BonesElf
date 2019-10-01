using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public Canvas canvas;
    public SceneEnum scene;

    public float changeTimer = 2f;

    GameObject panelMushroomFade;
    void Start()
    {
        if(canvas.transform.Find("PanelMushroomFade") != null)
        {
            panelMushroomFade = canvas.transform.Find("PanelMushroomFade").gameObject;
        }
        else
        {
            panelMushroomFade = null;       
        }
    }
    
    public void LoadNewScene()
    {
        StartCoroutine(CallFadeScene(changeTimer, scene.ToString()));
    }

    public void LoadNewSceneWithCustomTimer(float timer){
        StartCoroutine(CallFadeScene(timer, scene.ToString()));
    }

    public void LoadSceneWithParams(string scene, float timer)
    {
        StartCoroutine(CallFadeScene(timer, scene));
    }

    public void FadeSceneOut(float fateTime){
        Fader fader = CreateBackgroundElementToFade().AddComponent<Fader>();
        fader.fadeObjectWithChilds(FadeDirection.Out, fateTime);
    }

    IEnumerator CallFadeScene(float time, string newScene)
    {
        float fadeDuration = 2f;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        Debug.Log(time);

        if(panelMushroomFade != null)
        {
            panelMushroomFade.SetActive(true);
            panelMushroomFade.GetComponent<Animator>().Play("FadeIn");
        }
        else
        {
            Fader fader = CreateBackgroundElementToFade().AddComponent<Fader>();
            fader.fadeObjectWithChilds(FadeDirection.In, fadeDuration);
        }
        
        yield return new WaitForSeconds(fadeDuration);

        ChangeScene(newScene);
    }

    public void ChangeScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public GameObject CreateBackgroundElementToFade(){
        GameObject sceneFaderBackground = new GameObject("sceneFaderBackground");
        sceneFaderBackground.transform.SetParent(canvas.transform);

        RectTransform rt = sceneFaderBackground.AddComponent<RectTransform>();
        RectTransform canvasRt = canvas.GetComponent<RectTransform>();

        rt.sizeDelta = new Vector2(canvasRt.sizeDelta.x, canvasRt.sizeDelta.y);
        rt.transform.position  = canvasRt.transform.position;
        rt.transform.localScale = Vector3.one;

        Image img = sceneFaderBackground.AddComponent<Image>();

        img.color = Color.black;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);

        return sceneFaderBackground;
    }
}
