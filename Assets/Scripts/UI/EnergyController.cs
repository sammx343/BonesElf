using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour {
    public GameObject energyBarContainer;
    public Sprite sprite;
    public GameObject bar;
    public GameObject energy;

    private PlayerController playerController;
    private PlayerController.PlayerAction lastPlayerAction;
    private int squareSize = 30;
    public int sizeSetter = 3;
    private int squaresNumber;
    private float jumpEnergy = 0.8f;
    private float hangUpEnergy = 0.014f;
    private float energyRecoverySpeed = 0.005f;
    private float currentEnergy;
    private List<GameObject> energySquares;

    // Use this for initialization
    void Start () {
        energySquares = new List<GameObject>();
        playerController = GetComponent<PlayerController>();
        CreateEnergyBar();
    }
	
	// Update is called once per frame
	void Update () {
        //If the action is different than the last one, compare actions and spend energy
        if(lastPlayerAction != playerController.playerAction)
        {
            lastPlayerAction = playerController.playerAction;
            SpendEnergy(lastPlayerAction);
        }//but, if it is hanging up, spend energy continuously 
        else if (lastPlayerAction == PlayerController.PlayerAction.HangUp)
        {
            ChangeEnergyBar(-hangUpEnergy);
        }

        FillEnergyBar();
        currentEnergy = bar.transform.localScale.x;
    }

    public void CreateEnergyBar()
    {
        energySquares.ForEach(energySquare =>
        {
            Destroy(energySquare);
        });

        lastPlayerAction = playerController.playerAction;

        squaresNumber = (int)(ElfStatus.maxTotalEnergy / 20);

        int firstSquarePosition = -squaresNumber / 2;

        RectTransform rect = bar.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(firstSquarePosition * squareSize - rect.sizeDelta.x / 2, rect.localPosition.y, rect.localPosition.z);
        CompletelyFillEnergyBar();

        for (int i = 0; i < squaresNumber; i++)
        {
            energySquares.Add(CreateEnergySquares("Square" + i, i, firstSquarePosition));
        }
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

    //Recovers energy bar by an amout
    private void FillEnergyBar()
    {
        if (bar.transform.localScale.x < squaresNumber)
        {
            ChangeEnergyBar(energyRecoverySpeed);
        }
    }
    //Completely recovers energy bar
    private void CompletelyFillEnergyBar()
    {
        RectTransform rect = bar.GetComponent<RectTransform>();
        bar.transform.localScale = new Vector2(squaresNumber, bar.transform.localScale.y);
    }


    private void ChangeEnergyBar(float change)
    {
        RectTransform rect = bar.GetComponent<RectTransform>();

        currentEnergy += change;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        else if (currentEnergy > squaresNumber)
        {
            currentEnergy = squaresNumber;
        }
        bar.transform.localScale = new Vector2(currentEnergy, bar.transform.localScale.y);
    }

    //Takes Energy when player is doing certain actions
    private void SpendEnergy(PlayerController.PlayerAction lastPlayerAction)
    {
        switch (lastPlayerAction) {
            case PlayerController.PlayerAction.Jump:
                ChangeEnergyBar(-jumpEnergy);
                break;
        }
    }

    private GameObject CreateEnergySquares(string name, int position, int firstPosition)
    {
        //Creates game object square and adds different properties
        GameObject squareObject = new GameObject(name);
        SpriteRenderer renderer = squareObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        RectTransform rt = squareObject.AddComponent<RectTransform>();
        Image image = squareObject.AddComponent<Image>();
        image.sprite = sprite;

        //Size of the square
        Vector2 squareDimension = new Vector2(squareSize, squareSize + sizeSetter);
        rt.sizeDelta = squareDimension;

        Vector3 squarePosition = new Vector3((firstPosition + position)* (squareSize-sizeSetter), 0, 0);
        GameObject createdSquare = Instantiate(squareObject, squarePosition, energyBarContainer.transform.rotation) as GameObject;
        //GameObject createdSquare = Instantiate(squareObject, lifeBar.transform) as GameObject;
        createdSquare.transform.SetParent(energyBarContainer.transform, false);

        return createdSquare;
    }
}
