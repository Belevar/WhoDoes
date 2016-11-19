using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuessNumber : MonoBehaviour {

    int hiddenNumber;
    int minValue;
    public int maxValue = 20;
    public TurnsHandler turnHandler;
    List<Player> players;
    List<int> guesses;
    bool turnInProgress;
    Player currentPlayer;
    

	// Use this for initialization
	void Start () {
        hiddenNumber = Random.Range(minValue, maxValue);
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnHandler.SetNumberOfTurnsInGame(1);
        turnHandler.SetNumberOfPlayersInGame(players.Count);
        turnInProgress = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (turnHandler.ifThereIsNextPlayer())
        {
            if (!turnInProgress)
            {
                SetTurn();
                SetScene();
            } else
            {
                GetPlayerGuess();
            }

        } else
        {
            ShowLooser();
        }
	}

    void SetScene()
    {
        Debug.Log("Set Scene");
    }


    void SetTurn()
    {
        currentPlayer = turnHandler.ReturnCurrentPlayerTurn();
        turnInProgress = true;
    }

    void GetPlayerGuess()
    {
        int guess = 0;
        guesses.Add(guess);
        EndTurn();
    }

    void EndTurn()
    {
        turnInProgress = false;
    }

    void ShowLooser()
    {
        Debug.Log("Show looser");
    }
}
