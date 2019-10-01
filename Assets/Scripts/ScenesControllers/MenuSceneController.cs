using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneController : MonoBehaviour {

	public GameObject firstWelcomeText;
	public GameObject welcomePanel;
	public GameObject continuePanel;

	// Use this for initialization
	void Start () {
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.FadeSceneOut(2f);

		Text welcomeText = firstWelcomeText.GetComponent<Text>();

		string nickName = ElfStatus.currentUser.NickName;
		Debug.Log(ElfStatus.currentScene);

		if( string.IsNullOrEmpty(ElfStatus.currentScene) )
		{
			welcomePanel.SetActive(true);
			welcomeText.text = "!Bienvenido " + nickName + "!";
		}
		else
		{
			welcomeText.text = "!Hola de nuevo " + ElfStatus.currentUser.NickName + "!";
			continuePanel.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	public void ChangeScene()
	{
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.LoadNewScene();
	}

	public void LoadNewGame()
	{
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.LoadSceneWithParams(SceneEnum.SignUp.ToString(), 1f);
	}
}
