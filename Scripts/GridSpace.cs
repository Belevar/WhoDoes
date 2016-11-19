using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText;
    public Sprite[] images;

    private GameController gameController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSpace()
    {
        SpriteState spriteState = new SpriteState();
        spriteState = button.spriteState;
        if (gameController.GetPlayerSide() == "X")
        {
            buttonText.text = "X";
            spriteState.pressedSprite = images[1];
            GetComponent<Image>().sprite = images[1];
        }
        else
        {
            buttonText.text = "O";
            spriteState.pressedSprite = images[0];
            GetComponent<Image>().sprite = images[0];
        }
        buttonText.gameObject.SetActive(false);
        button.spriteState = spriteState;
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
