using UnityEngine;
public class ElfStatus  : MonoBehaviour 
{
    public static int baseLife = 3;
    public static int baseEnergy = 3;
    public static int maxTotalEnergy = baseEnergy;
    public static int maxTotalLife = baseLife;

    public static Vector3 checkPointPosition;

    public static bool isCheckPointActive;
}

public enum ElfStat
{
    baseLife,
    baseEnergy
}