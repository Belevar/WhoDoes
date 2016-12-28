using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

public class PlayersManager : MonoBehaviour {

    public GameObject addPlayerButton;
    public Toggle savePlayersToggle;
    public Transform playersPlaceholder;
    
    bool playersNamesAreSaved;
    bool savePlayers;

	static PlayersManager instance = null;
    PlayersHanlder players = new PlayersHanlder();
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Destroy Players Manager on AWAKE");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            CheckIfThereAreSavedPlayers();
            savePlayers = savePlayersToggle.isOn;
            if (playersNamesAreSaved)
            {
                LoadPlayers();
            }
            DontDestroyOnLoad(this);

        }
        
    }

    void LoadPlayers()
    {
        Debug.Log("Load Players");
        XmlReader reader = XmlReader.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "players.xml");
        try
        {
            XmlSerializer s = new XmlSerializer(typeof(PlayersHanlder));
            players = (PlayersHanlder)s.Deserialize(reader);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            players = new PlayersHanlder();
        }
        finally
        {
            reader.Close();
        }
    }

    
    void CheckIfThereAreSavedPlayers()
    {
        playersNamesAreSaved = PlayerPrefsManager.CheckIfPlayerNamesAreSaved();
    }


    public void SavePlayers()
    {
        Debug.Log("Save Players");
        
        Debug.Log(Application.persistentDataPath);
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + Path.DirectorySeparatorChar + "players.xml");
        try
        {
            XmlSerializer serializator = new XmlSerializer(typeof(PlayersHanlder));
            serializator.Serialize(sw, players);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            sw.Close();
        }
    }

    public void CollectPlayerNames()
    {
        players.players.Clear();
        savePlayers = savePlayersToggle.isOn;
        Debug.Log("Save Players " + savePlayers);


        PlayerPrefsManager.savePlayerNames(savePlayers.ToString());
        foreach (RectTransform playerField in playersPlaceholder)
        {
            //string defaultText = playerField.GetChild(0).GetComponentInChildren<Text>().text;
            //Text userText = playerField.GetChild(0).GetComponent<InputField>().textComponent;

            string defaultText = playerField.GetComponentInChildren<InputField>().placeholder.GetComponent<Text>().text;
            Debug.Log("Default text=" + defaultText);
            string userText = playerField.GetChild(0).GetComponentInChildren<InputField>().text;
            Debug.Log("text=" +  userText);
            Debug.Log("Text comp=" + playerField.GetChild(0).GetComponentInChildren<InputField>().textComponent);
            Debug.Log(playerField.GetComponentInChildren<SexButton>().GetSex());
            
           if(userText.Equals(""))
           {
               players.players.Add(new Player() { playerName = defaultText, sex = playerField.GetComponentInChildren<SexButton>().GetSex() });
           } else
           {
               players.players.Add(new Player() { playerName = userText, sex = playerField.GetComponentInChildren<SexButton>().GetSex() });
           }
        }

        if(savePlayers)
        {
            SavePlayers();
        }

        foreach (var item in players.players)
        {
            Debug.Log("Player name= " + item.playerName + ", sex=" +  item.sex);
        }
    }

    public List<Player> GetPlayers()
    {
        return players.players;
    }
}
