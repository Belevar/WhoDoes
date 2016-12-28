using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Boombs : MonoBehaviour {

    public List<BombField> bombsFields;
    public Sprite womanSprite;
    public Transform bombsFieldsHandler;
    public Image avatar;
    public Text PlayerName;


    Sprite manSprite;
    bool turnInProgress;
    int currentPlayer = 0;
    bool boombFound = false;
    List<Player> players;
    

	// Use this for initialization
	void Start () {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnInProgress = false;
        bombsFields = new List<BombField>();
        foreach (RectTransform bombField in bombsFieldsHandler)
        {
            bombsFields.Add(bombField.GetComponent<BombField>());
        }
        Debug.Log("Ilośc pól minowych" + bombsFields.Count);
        int bombPlace = Random.Range(0, bombsFields.Count);
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

    float timeOutBeforeEndGameScene = 1f;
    float currentTime = 0f;

    void ShowLooser()
    {
        Debug.Log("Player - " + players[currentPlayer].playerName + " lost");
        
        if(currentTime > timeOutBeforeEndGameScene)
        {
            FindObjectOfType<GameResult>().setLooser(players[currentPlayer].playerName, players[currentPlayer].sex);
            FindObjectOfType<LevelManager>().LoadScene("end_game");
        }
        currentTime += Time.deltaTime;

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
