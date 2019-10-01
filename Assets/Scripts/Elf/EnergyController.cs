using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour {

    private PlayerController playerController;
    private PlayerController.PlayerAction lastPlayerAction;
    private float maxTotalEnergy = ElfStatus.maxTotalEnergy;

    public GameObject energyBar;
    public float currentEnergy = ElfStatus.maxTotalEnergy;
    public float jumpEnergy = 0.8f;
    public float hangUpEnergy = 0.014f;
    public float energyRecoverySpeed = 0.02f;
    
    // Use this for initialization
    void Start ()
    {
        playerController = GetComponent<PlayerController>();
        lastPlayerAction = playerController.playerAction;
        energyBar.GetComponent<BarController>().barQuantity = currentEnergy;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (lastPlayerAction != playerController.playerAction)
        {
            lastPlayerAction = playerController.playerAction;
            if (HasEnergyForAction(lastPlayerAction)) SpendEnergy(lastPlayerAction);
        }
        else if (lastPlayerAction == PlayerController.PlayerAction.HangUp)
        {
            ChangeEnergy(-hangUpEnergy);
        }

        FillEnergy();
        energyBar.GetComponent<BarController>().barQuantity = currentEnergy;
    }

    public bool HasEnergyForAction(PlayerController.PlayerAction lastPlayerAction)
    {
        float energyToSpend = 0;
        switch (lastPlayerAction)
        {
            case PlayerController.PlayerAction.Jump:
                energyToSpend = jumpEnergy;
                break;
        }
        return currentEnergy > energyToSpend;
    }

    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public float GetEnergyRecoverySpeed()
    {
        return energyRecoverySpeed;
    }

    public void CreateEnergyBar()
    {
        maxTotalEnergy = ElfStatus.maxTotalEnergy;
        currentEnergy = maxTotalEnergy;
        energyBar.GetComponent<BarController>().CreateBar();
    }

    //Takes Energy when player is doing certain actions
    private void SpendEnergy(PlayerController.PlayerAction lastPlayerAction)
    {
        switch (lastPlayerAction)
        {
            case PlayerController.PlayerAction.Jump:
                ChangeEnergy(-jumpEnergy);
                break;
        }
    }

    //Recovers energy bar by an amout
    public void FillEnergy()
    {
        ChangeEnergy(energyRecoverySpeed);
    }
    
    public void FillEnergyCompletely()
    {
        ChangeEnergy(maxTotalEnergy);
    }

    private void ChangeEnergy(float change)
    {
        currentEnergy += change;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        else if (currentEnergy > maxTotalEnergy)
        {
            currentEnergy = maxTotalEnergy;
        }
    }
}
