using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Baloons : MonoBehaviour {

    public TurnsHandler turnHandler;
    public GameObject baloonPrefab;
    public float xMin, xMax, yPos, minVelocity, maxVelocity;
    List<Player> players;
    List<int> results;
    bool turnInProgress;
    Player currentPlayer;

    int destroyedBaloons;
    float currentTurnTime = 0f;
    float turnTime;

    // Use this for initialization
    void Start()
    {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        turnHandler.SetNumberOfTurnsInGame(1);
        turnHandler.SetNumberOfPlayersInGame(players.Count);
        turnInProgress = false;
        destroyedBaloons = 0;
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
                SpawnBallonsForTurnDuration();
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

    void EndTurn()
    {
        destroyedBaloons = 0;
        turnInProgress = false;
    }

    void ShowLooser()
    {
        Debug.Log("Show looser");
    }

    void SpawnBallon()
    {
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), yPos);
        GameObject ballon = Instantiate(baloonPrefab,pos, Quaternion.identity) as GameObject;
        ballon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(minVelocity, maxVelocity));
    }

    void SpawnBallonsForTurnDuration()
    {
        if (currentTurnTime < turnTime)
        {
            SpawnBallon();
        } else
        {
            //SAVE PLAYER SCORE
            currentTurnTime = 0;
            EndTurn();
        }
    }
}
