using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BombField : MonoBehaviour {

    public Sprite bombSprite;

    public Boombs gameController;
    public bool hasBomb {get; set;}

    // Use this for initialization
    void Start()
    {
        hasBomb = false;
    }

	// Update is called once per frame
	void Update () {
	
	}


    public void CheckForBomb()
    {
        if(hasBomb)
        {
            Debug.Log("HAS BOMB");
            GetComponent<Image>().sprite = bombSprite;
            GetComponent<Button>().interactable = false;
            gameController.EndGame();
        } else
        {
            Debug.Log("NO BOMB");
            GetComponent<Button>().interactable = false;
            gameController.EndTurn();
        }
    }

}
