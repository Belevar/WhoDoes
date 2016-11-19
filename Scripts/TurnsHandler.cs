using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnsHandler : MonoBehaviour {

    List<Player> players;
    int currentPlayer = 0;
    int currentTurn = 0;
    int maxPlayersInGame = 2;
    int maxTurns = 1;

	// Use this for initialization
	void Start () {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public Player ReturnCurrentPlayerTurn()
    {
        Player nextPlayer = players[currentPlayer];
        if (++currentPlayer >= players.Count)
        {
            currentPlayer = 0;
            currentTurn++;
            //Do some stuff here
        }
        return nextPlayer;
    }

    public void SetNumberOfPlayersInGame(int numberOfPlayers)
    {
        maxPlayersInGame = numberOfPlayers;
    }

    public void SetNumberOfTurnsInGame(int turns)
    {
        maxTurns = turns;
    }

    public bool ifThereIsNextPlayer()
    {
        return (currentPlayer < maxPlayersInGame && currentTurn < maxTurns) ? true : false;
    }
}
