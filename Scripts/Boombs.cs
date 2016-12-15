using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Boombs : MonoBehaviour {

    public List<BombField> bombsFields;
    public Sprite womanSprite;
    Sprite manSprite;
    bool turnInProgress;
    public Image avatar;
    public Text PlayerName;
    int currentPlayer = 0;
    bool boombFound = false;
    List<Player> players;


	// Use this for initialization
	void Start () {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnInProgress = false;
        int bombPlace = Random.Range(0, 20);
        bombsFields[bombPlace].hasBomb = true;
        Debug.Log("Bomb is under " + (bombPlace + 1));
        manSprite = avatar.sprite;
        FillPlayerTemplate(players[currentPlayer]);
	}
	
	// Update is called once per frame
	void Update () {
        if (!boombFound)
        {
            if (!turnInProgress)
            {
                SetTurn();
            }
        }
        else
        {
            DisactivateButtons();
            ShowLooser();
        }
	}

    void DisactivateButtons()
    {
        foreach (BombField button in bombsFields)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }
    
    void FillPlayerTemplate(Player player)
    {
        if (player.sex == "woman")
        {
            avatar.sprite = womanSprite;
        } else
        {
            avatar.sprite = manSprite;
        }
        PlayerName.text = player.playerName;
    }

    void ShowLooser()
    {
        Debug.Log("Player - " + players[currentPlayer].playerName + " lost");
    }


    void SetTurn()
    {
        turnInProgress = true;
    }


    public void EndGame()
    {
      
        turnInProgress = false;
        boombFound = true;
    }

    public void EndTurn()
    {
        currentPlayer = ++currentPlayer % players.Count;
        FillPlayerTemplate(players[currentPlayer]);
        Debug.Log("Next Player: " + players[currentPlayer].playerName);
        turnInProgress = false;
    }
}
