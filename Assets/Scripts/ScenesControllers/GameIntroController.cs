using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameStatus gameStatus = SaveSystem.LoadGameStatus();

		SceneChanger changer = gameObject.GetComponent<SceneChanger>();
		if(gameStatus != null)
		{
			Debug.Log(JsonUtility.ToJson(gameStatus));
			
			changer.LoadNewScene();

			User user = new User();
			user.FirstName = gameStatus.userFirstName;
			user.LastName = gameStatus.userLastName;
			user.NickName = gameStatus.userNickName;

			ElfStatus.SetAttributes((int)gameStatus.maxHealth, (int)gameStatus.maxEnergy, gameStatus.position, user, gameStatus.userKey, gameStatus.scene);
		}
		else
		{
			changer.LoadSceneWithParams(SceneEnum.SignUp.ToString());
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
