using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {
    private PlayerController playerController;
    private PlayerController.PlayerAction lastPlayerAction;
    private float maxTotalLife = ElfStatus.maxTotalLife;

    public float currentLife = ElfStatus.maxTotalLife;
    public float lifeRecoverySpeed = 0.005f;

    public GameObject lifeBar;

    // Use this for initialization
    void Start () {
        playerController = GetComponent<PlayerController>();
        lastPlayerAction = playerController.playerAction;
        lifeBar.GetComponent<BarController>().barQuantity = currentLife;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
