using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIntroController : MonoBehaviour {
	public GameObject textObject;
	public GameObject gameTitle;
	GameStatus gameStatus;
	// Use this for initialization
	void Start () 
	{
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		changer.FadeSceneOut(2f);

		gameStatus = SaveSystem.LoadGameStatus();

		Environment environment = new Environment();
		Debug.Log(Environment.instance.GetPressIntroText());
		textObject.GetComponent<Text>().text = Environment.instance.GetPressIntroText();
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
		if( userAction ) 
		{
			startGame();
		}
	}

	void startGame()
	{
		gameTitle.GetComponent<Animator>().SetBool("IsPressed", true);
		textObject.SetActive(false);
		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		if(gameStatus != null)
		{
			Debug.Log(JsonUtility.ToJson(gameStatus));
			
			changer.LoadNewSceneWithCustomTimer(1f);

			User user = new User();
			user.FirstName = gameStatus.userFirstName;
			user.LastName = gameStatus.userLastName;
			user.NickName = gameStatus.userNickName;

			ElfStatus.SetAttributes((int)gameStatus.maxHealth, (int)gameStatus.maxEnergy, gameStatus.position, user, gameStatus.userKey, gameStatus.scene);
		}
		else
		{
			changer.LoadSceneWithParams(SceneEnum.SignUp.ToString(), 1f);
		}
	}
}
