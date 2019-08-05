using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TransitionController : MonoBehaviour {
    public VideoPlayer vp;
    public VideoPlayer vp2;
    // Use this for initialization
    void Start () {
        //vp.frame = (long)vp.frameCount;
        vp.loopPointReached += EndReached;
        vp2.loopPointReached += EndReached;
        vp.targetCameraAlpha = 0.8F;
        vp.playOnAwake = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(vp.frame + 10 > (long)vp.frameCount)
        {
            vp2.Play();
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.enabled = false;
        Debug.Log("End Reached");
    }
}
