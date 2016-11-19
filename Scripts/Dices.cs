using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dices : MonoBehaviour {

    public TurnsHandler turnHandler;
    List<Player> players;
    List<int> results;
    bool turnInProgress;
    Player currentPlayer;


    // Use this for initialization
    void Start()
    {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnHandler.SetNumberOfTurnsInGame(1);
        turnHandler.SetNumberOfPlayersInGame(players.Count);
        turnInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnHandler.ifThereIsNextPlayer())
        {
            if (!turnInProgress)
            {
                SetTurn();
                SetScene();
            }
            else
            {
                ThrowDices();
            }

        }
        else
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

    void ThrowDices()
    {
        //here code about shaking phone
        int dicesValue = 0;
        results.Add(dicesValue);
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
