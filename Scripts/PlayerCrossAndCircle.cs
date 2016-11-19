using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerCrossAndCircle : MonoBehaviour {

    public Text playerName;
    public Image avatar;
    public Image circleOrCross;
    public Sprite womanImage;
    public int wins = 0;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
        fillPlayerTemplate();	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void fillPlayerTemplate()
    {
        List<Player> players = FindObjectOfType<PlayersManager>().GetPlayers();
        if (gameObject.name.Contains("Circle"))
        {
            playerName.text = players[0].playerName;
            assignCorrectAvatar(players[0].sex);
        }
        else
        {
            playerName.text = players[1].playerName;
            assignCorrectAvatar(players[1].sex);
        }
    }


    public void assignCorrectAvatar(string sex)
    {
        if (sex == "woman")
        {
            avatar.sprite = womanImage;
        }
    }

    public void greyOutPlayer()
    {
        Debug.Log("Grey out " + name);
        GetComponent<CanvasRenderer>().SetColor(Color.gray);
        avatar.GetComponent<CanvasRenderer>().SetColor(Color.gray);
        circleOrCross.GetComponent<CanvasRenderer>().SetColor(Color.gray);
    }

    public void colorPlayer()
    {
        Debug.Log("color full " + name);
        GetComponent<CanvasRenderer>().SetColor(Color.white);
        avatar.GetComponent<CanvasRenderer>().SetColor(Color.white);
        circleOrCross.GetComponent<CanvasRenderer>().SetColor(Color.white);
    }
}
