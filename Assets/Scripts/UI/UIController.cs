using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    public GameObject UIPausePanel;
    public GameObject buttonPause;

    private float CurrentTimeScale;
    

    // Use this for initialization
    void Start () {
        CurrentTimeScale = Time.timeScale;
    }

    public void GamePause(bool shouldPause)
    {
        SwitchUIState(shouldPause);
        if (shouldPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = CurrentTimeScale;
        }
    }

    private void SwitchUIState(bool dialogIsActive)
    {
        buttonPause.SetActive(!dialogIsActive);
        UIPausePanel.SetActive(dialogIsActive);
    }
}
