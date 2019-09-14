using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour {
	static string fileName = "GameStatus.data";
	public static void SaveGameStatus (string scene, int maxHealth, int maxEnergy, Vector3 position, User user, string userKey)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		
		string path = Path.Combine(Application.persistentDataPath, fileName);

		using(FileStream stream = new FileStream(path, FileMode.Create))
		{
			GameStatus data = new GameStatus(scene, maxHealth, maxEnergy, position, user, userKey);
			formatter.Serialize(stream, data);
			stream.Close();
		}
	}

	public static GameStatus LoadGameStatus (){

		string path = Path.Combine(Application.persistentDataPath, fileName);

		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			using(FileStream stream = new FileStream(path, FileMode.Open))
			{
				GameStatus gameStatus = formatter.Deserialize(stream) as GameStatus;
				stream.Close();
				return gameStatus;
			}
		}
		else
		{
			Debug.Log("Save file not found in " + path);
			return null;
		}
	}
}
