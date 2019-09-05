using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour {
    public GameObject barContainer;
    public GameObject elementQuantity; //Cantidad del elemento en color
    public GameObject squareContainer;
    public ElfStat elfStat;
    
    private float squareQuantity;
    public float barQuantity;

    private List<GameObject> squares;

    private int widthUnit = 20;
    private int heightUnit = 20;
    public int squareOverlapDistance = 2;

    // Use this for initialization
    void Start ()
    {
        squares = new List<GameObject>();
        //Width of the elementQuantity should be touched only on start, its changes with the container scale
        AdjustElementWidth();
        CreateBar();
    }
	
	// Update is called once per frame
	void Update()
    { 
        ChangeBarQuantity(barQuantity);
    }

    public float GetCurrentBarQuantity()
    {
        return barQuantity;
    }

    //Adjust the width of the bar and sets its position inside the container
    void AdjustElementWidth()
    {
        //Sets the WITDH and HEIGHT of Rect Transform element
        elementQuantity.GetComponent<RectTransform>().sizeDelta = new Vector2(widthUnit, heightUnit);

        //Changes the position of the Bar to always be at half of his size
        //The anchor position has to be center center
        RectTransform customRect = elementQuantity.GetComponent<RectTransform>();
        var containerSize = customRect.rect.width;
        customRect.localPosition = new Vector3(containerSize / 2, customRect.localPosition.y, customRect.localPosition.z);
    }

    public void CreateBar()
    {
        squares.ForEach(energySquare =>
        {
            Destroy(energySquare);
        });

        if (elfStat == ElfStat.baseEnergy)
        {
            squareQuantity = ElfStatus.maxTotalEnergy;
        }
        else if (elfStat == ElfStat.baseLife)
        {
            squareQuantity = ElfStatus.maxTotalLife;
        }

        FillBarCompletely();
        for (int i = 0; i < squareQuantity; i++)
        {
            squares.Add(CreateWhiteSquares("Square" + i, i));
        }
    }

    //Completely recovers energy bar
    private void FillBarCompletely()
    {
        barContainer.transform.localScale = new Vector2(squareQuantity, barContainer.transform.localScale.y);
    }

    private void ChangeBarQuantity(float change)
    {
        barContainer.transform.localScale = new Vector2(change, barContainer.transform.localScale.y);
    }

    private GameObject CreateWhiteSquares(string name, int position)
    {
        //Creates the squares and sets the parent and sizes
        GameObject squareObject = new GameObject(name);
        squareObject.transform.SetParent(squareContainer.transform, false);

        //Creates a RectTransform to manipulate the position and the sizes
        RectTransform rt = squareObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(widthUnit + squareOverlapDistance, heightUnit);
        
        rt.localPosition = new Vector3(position * (widthUnit), rt.localPosition.y, rt.localPosition.z);

        //Sets the image
        Image squareImage = squareObject.AddComponent<Image>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("barra_energia");
        squareImage.sprite = sprites[12];

        return squareObject;
    }
}
