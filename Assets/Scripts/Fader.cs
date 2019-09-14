using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fader : MonoBehaviour {

    private float fadeSpeed = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void fadeObjectWithChilds(FadeDirection direction, float fadeSpeed)
	{
		this.fadeSpeed = fadeSpeed;
		StartCoroutine(Fade(direction, gameObject));
		foreach (Transform child in transform)
		{
        	StartCoroutine(Fade(direction, child.gameObject));
		}
    }

	public void fadeCustomObject(GameObject customGameObject, FadeDirection direction, float fadeSpeed)
	{
		this.fadeSpeed = fadeSpeed;
		StartCoroutine(Fade(direction, customGameObject));
	}
	
    private IEnumerator Fade(FadeDirection fadeDirection, GameObject gameObject)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
		var image = gameObject.GetComponent<Image>();
		
        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection, image);
                yield return null;
            }

            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection, image);
                yield return null;
            }
        }
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection, Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        alpha += Time.deltaTime * (1f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
}
