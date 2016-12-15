﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Baloons : MonoBehaviour {

    public GameObject baloonPrefab;
    public float xMin, xMax, yPos, minVelocity, maxVelocity;
    public Sprite womanSprite;
    public Text timer;
    Sprite manSprite;
    public Image avatar;
    public Text PlayerName;
    List<Player> players;
    List<int> results;
    bool turnInProgress;
    int currentPlayer;

    int destroyedBaloons;
    float currentTurnTime = 0f;
    float turnTime;

    // Use this for initialization
    void Start()
    {
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        currentPlayer = 0;
        turnTime = 10.0f;
        turnInProgress = false;
        destroyedBaloons = 0;
        manSprite = avatar.sprite;
        FillPlayerTemplate(players[currentPlayer]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer < players.Count)//turnHandler.ifThereIsNextPlayer())
        {
            if (!turnInProgress)
            {
                SetScene();
                if (Input.GetMouseButtonDown(0))
                {
                    turnInProgress = true;
                }
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

    void FillPlayerTemplate(Player player)
    {
        if (player.sex == "woman")
        {
            avatar.sprite = womanSprite;
        }
        else
        {
            avatar.sprite = manSprite;
        }
        PlayerName.text = player.playerName;
    }


    void SetScene()
    {
        Debug.Log("Set Scene");
        timer.text = "Tap to start";
    }

    void EndTurn()
    {
        destroyedBaloons = 0;
        currentTurnTime = 0f;
        currentPlayer++;
        FillPlayerTemplate(players[currentPlayer]);
        turnInProgress = false;
    }

    void ShowLooser()
    {
        int worstPlayer = 0;
        int worstPlayerScore = 0;
        for (int i = 0; i < results.Count; i++)
        {
            if(worstPlayerScore > results[i])
            {
                worstPlayer = i;
                worstPlayerScore = results[i];
            }
        }
        Debug.Log("Worst player name: " + players[worstPlayer].playerName + " score" + worstPlayerScore);
    }

    void SpawnBallon()
    {
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), yPos);
        GameObject ballon = Instantiate(baloonPrefab,pos, Quaternion.identity) as GameObject;
        ballon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(minVelocity, maxVelocity));
        Debug.Log("HUE HUE HUE SPAWN");
    }

    void SpawnBallonsForTurnDuration()
    {
        if (currentTurnTime < turnTime)
        {
            currentTurnTime += Time.deltaTime;
            timer.text = (turnTime - currentTurnTime).ToString("0.00");
            if((int)currentTurnTime % 2 == 0)
            {
                SpawnBallon();
            }
 
        } else
        {
            //SAVE PLAYER SCORE
            currentTurnTime = 0;
            EndTurn();
        }
    }
}
