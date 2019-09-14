using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatus {

	public string scene;
	public int maxHealth;
	public int maxEnergy;
	public float[] position;

	public string userNickName;
	public string userFirstName;
	public string userLastName;
	public string userKey;
	
	public GameStatus(string scene, int maxHealth, int maxEnergy, Vector3 position, User user, string userKey){
		this.scene = scene;
		this.maxHealth = maxHealth;
		this.maxEnergy = maxEnergy;

		this.position = new float[3];
		this.position[0] = position.x;
		this.position[1] = position.y;
		this.position[2] = position.z;

		userNickName = user.NickName;
		userFirstName = user.FirstName;
		userLastName = user.LastName;
		this.userKey = userKey;
	}
}
