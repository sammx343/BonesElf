using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {
    public float velocityX = 0;
    public bool isGenerated = false;
    // Use this for initialization
    
    public Utilities()
    {

    }

    public float RandomNumber(float min, float max)
    {
        return Random.Range(min, max);
    }
}
