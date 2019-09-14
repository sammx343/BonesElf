using UnityEngine;
public class ElfStatus  : MonoBehaviour 
{
    public  static readonly int  baseLife = 3;
    public static readonly int baseEnergy = 3;
    public static int maxTotalLife = baseLife;
    public static int maxTotalEnergy = baseEnergy;
    public static Vector3 checkPointPosition;
    public static User currentUser;
    public static string userKey;
    public static string currentScene;

    public static void SetAttributes(int life, int energy, float[] position, User user, string key, string scene)
    {
        maxTotalLife = life;
        maxTotalEnergy = energy;

        checkPointPosition.x = position[0];
        checkPointPosition.y = position[1];
        checkPointPosition.z = position[2];

        currentUser= user;
        userKey = key;
        currentScene = scene;
    }

    public static void SaveSelfGameStatus(string scene, Vector3 position)
    {
        Debug.Log(position);
        SaveSystem.SaveGameStatus(scene, maxTotalLife, maxTotalEnergy, position, currentUser, userKey);
    }
}

public enum ElfStat
{
    baseLife,
    baseEnergy
}