using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GuessNumber : MonoBehaviour {

    int hiddenNumber;
    int minValue;
    public int maxValue = 20;
    public TurnsHandler turnHandler;
    public List<PlayerGuessNumber> playersFields;
    List<Player> players;
    List<int> guesses;
    public bool turnInProgress;
    //Player currentPlayer;
    int currentPlayer = 0;
    float guessTime = 3;

	// Use this for initialization
	void Start () {
        guesses = new List<int>();
        hiddenNumber = Random.Range(minValue, maxValue);
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnHandler.SetNumberOfTurnsInGame(1);
        turnHandler.SetNumberOfPlayersInGame(players.Count);
        turnInProgress = false;
        for (int i = 0; i < players.Count; i++)
        {
            playersFields[i].fillPlayerTemplate(players[i].playerName, players[i].sex);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (currentPlayer < players.Count)//turnHandler.ifThereIsNextPlayer())
        {
            if (!turnInProgress)
            {
                SetTurn();
                SetScene();
            } 

        } else
        {
            ShowLooser();
        }
	}

    void SetScene()
    {
        Debug.Log("Set Scene");
        playersFields[currentPlayer].greyOutPlayer();
    }


    void SetTurn()
    {
        //currentPlayer = turnHandler.ReturnCurrentPlayerTurn();
        turnInProgress = true;
    }

    public void GetPlayerGuess(int playerGuess)
    {
        guesses.Add(playerGuess);
        playersFields[currentPlayer].guessText.text = " Guess:" + playerGuess;
        EndTurn();
    }

    void EndTurn()
    {
        playersFields[currentPlayer].colorPlayer();
        currentPlayer++;
        turnInProgress = false;
    }

    void ShowLooser()
    {
        Debug.Log("Show looser");
        GetComponent<Text>().text = "HIDDEN NUMBER IS " + hiddenNumber;
        CheckWhoLoose();
    }

    void CheckWhoLoose()
    {
        int looserIndex = 0;
        int looserDistanceValue = -1;
        for (int i = 0; i < guesses.Count; i++)
        {
            int tempDistance;
            if(guesses[i] <= hiddenNumber)
            {
                tempDistance = hiddenNumber - guesses[i];
            } else
            {
                tempDistance = guesses[i] - hiddenNumber;
            }

            if(tempDistance > looserDistanceValue)
            {
                looserDistanceValue = tempDistance;
                looserIndex = i;
            }
        }

        Debug.Log(players[looserIndex].playerName + "Lost!");
    }
}
