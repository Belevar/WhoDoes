using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayersFields : MonoBehaviour
{

    public GameObject playerField;
    public GameObject addPlayerButton;
    public Sprite womanSprite;
    public Transform playersPlaceholder;
    public float spaceBetweenFields = 100;
    public float moveUpSHIT = 100;

    PlayersManager playersManager;

    void Awake()
    {
        playersManager = FindObjectOfType<PlayersManager>();
        if (playersManager == null)
        {
            Debug.LogError("PLAYERS MANAGER NOT FOUND!");
        }
        
        //Debug.Log("Start space " + spaceBetweenFields);
    }

    // Use this for initialization
    void Start()
    {
        spaceBetweenFields = transform.GetChild(0).transform.position.y - transform.GetChild(1).transform.position.y;
        populatePlayersFields();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewPlayerFromButton()
    {
        GameObject newField = AddNewPlayer("", "");
        newField.GetComponent<PlayerFieldTouchEvent>().moveNewPlayer();
    }

    public GameObject AddNewPlayer(string playerName, string sex)
    {
        Transform lastPlayerField = playersPlaceholder.GetChild(0);
        Vector3 newFieldPosition = lastPlayerField.position;

        //float playersFieldOffset = transform.GetChild(0).transform.position.y - transform.GetChild(1).transform.position.y;
        //playersFieldOffset *= playersPlaceholder.childCount;
        //newFieldPosition.y -= playersFieldOffset;
        
        
        float playersFieldOffset = spaceBetweenFields * playersPlaceholder.childCount;
        Debug.Log("Players field offset: " + playersFieldOffset + " - Child count:" + playersPlaceholder.childCount);
        newFieldPosition.y -= playersFieldOffset;


        
        
        //For swipe in animation if player is created from button;
        if (playerName.Equals("") && sex.Equals(""))
        {
            newFieldPosition.x = -640 * 2;
        }

        GameObject newField = Instantiate(playerField, newFieldPosition, Quaternion.identity, playersPlaceholder) as GameObject;
        if (playerName.Equals(""))
        {
            Text newPlayerName = newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
            newPlayerName.text = "Player" + playersPlaceholder.childCount.ToString();

        }
        else
        {
            Text newPlayerName = newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
            newPlayerName.text = playerName;
            newField.GetComponentInChildren<SexButton>().SetSex(sex);
        }

        newFieldPosition.x = 0;
        Debug.Log("New pos before method" + newFieldPosition);
        if (playersPlaceholder.childCount >= 6)
        {
            addPlayerButton.gameObject.SetActive(false);
        }
        return newField;
    }

    public void DeletePlayer()
    {
        if (playersPlaceholder.childCount > 2)
        {
            Transform lastPlayerField = playersPlaceholder.GetChild(playersPlaceholder.childCount - 1);
            Destroy(lastPlayerField.gameObject);
            addPlayerButton.SetActive(true);
        }
    }

    public void DeletePlayer(int index)
    {
        if (playersPlaceholder.childCount > 2)
        {
            Destroy(playersPlaceholder.GetChild(index).gameObject);
            addPlayerButton.SetActive(true);
        }
    }

    public void DeletePlayer(Transform objectToDelete)
    {
        if (playersPlaceholder.childCount > 2)
        {
            Destroy(objectToDelete.transform.gameObject);
            addPlayerButton.SetActive(true);
        }
    }

    void populatePlayersFields()
    {
        List<Player> players = playersManager.GetPlayers();

        if (players.Count > 0)
        {
            int i = 0;
            foreach (RectTransform playerField in playersPlaceholder)
            {
                playerField.GetChild(0).GetComponentInChildren<Text>().text = players[i].playerName;
                playerField.GetComponentInChildren<SexButton>().SetSex(players[i].sex);
                ++i;
            }
            if (players.Count > 2)
            {

                int defaultPlayersNum = 2;
                int numberOfFieldsToAdd = players.Count - defaultPlayersNum;
                for (i = 0; i < numberOfFieldsToAdd; i++)
                {
                    AddNewPlayer(players[i + 2].playerName, players[i + 2].sex);
                }
            }
        }
    }

}
