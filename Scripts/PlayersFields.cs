using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayersFields : MonoBehaviour {
    
    public GameObject playerField;
    public GameObject addPlayerButton;
    public Sprite womanSprite;
    public Transform playersPlaceholder;
    public int spaceBetweenFields = 105;

    PlayersManager playersManager;

    void Awake()
    {
        playersManager = FindObjectOfType<PlayersManager>();
        if(playersManager == null)
        {
            Debug.LogError("PLAYERS MANAGER NOT FOUND!");
        }
        
    }

    // Use this for initialization
    void Start () {
        populatePlayersFields();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddNewPlayerFromButton()
    {
        AddNewPlayer("", "");
    }

    public void AddNewPlayer(string playerName, string sex)
    {
        Transform button = addPlayerButton.transform;
       // button.position = new Vector3(button.position.x, button.position.y - spaceBetweenFields, button.position.z);
        Transform lastPlayerField = playersPlaceholder.GetChild(playersPlaceholder.childCount - 1);
        Vector3 newFieldPosition = lastPlayerField.position;
        newFieldPosition.y -= spaceBetweenFields;
        GameObject newField = Instantiate(playerField, newFieldPosition, Quaternion.identity, playersPlaceholder) as GameObject;
        if (playerName.Equals(""))
        {
            Text newPlayerName = newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
            newPlayerName.text = "Player" + playersPlaceholder.childCount.ToString();
            
        } else
        {
            Text newPlayerName = newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
            newPlayerName.text = playerName;
            newField.GetComponentInChildren<SexButton>().SetSex(sex);
        }

        if (playersPlaceholder.childCount >= 6)
        {
            button.gameObject.SetActive(false);
        }
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
                    AddNewPlayer(players[i + 2].playerName, players[i+2].sex);
                }
            }
        }
    }

}
