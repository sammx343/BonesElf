using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour {
    public GameObject lifeBar;
    public Sprite sprite;
    
    private PlayerController playerController;
    private PlayerController.PlayerAction lastPlayerAction;

    // Use this for initialization
    void Start () {
        playerController = GetComponent<PlayerController>();
        lastPlayerAction = playerController.playerAction;



        CreateLifeSquares("New1");
        CreateLifeSquares("New2");

        Debug.Log(lastPlayerAction);
    }
	
	// Update is called once per frame
	void Update () {
        if(lastPlayerAction != playerController.playerAction)
        {
            lastPlayerAction = playerController.playerAction;
            Debug.Log(lastPlayerAction);
        }
	}

    private void CreateLifeSquares(string name)
    {
        GameObject squareObject = new GameObject(name);
        SpriteRenderer renderer = squareObject.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;

        RectTransform rt = squareObject.AddComponent<RectTransform>();
        Image image = squareObject.AddComponent<Image>();
        image.sprite = sprite;
        rt.sizeDelta = new Vector2(30, 30);

        GameObject createdSquare = Instantiate(squareObject, lifeBar.transform) as GameObject;
        createdSquare.transform.parent = lifeBar.transform;
    }
}
