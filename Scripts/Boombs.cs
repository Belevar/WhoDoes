using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;

public class Boombs : MonoBehaviour {

	public List<BombField> bombsFields;
	public Sprite womanSprite;
	public Transform bombsFieldsHandler;
    public GameObject playerFieldTemplate;
    public Transform listOfPlayers;

	public Sprite manSprite;
	bool turnInProgress;
	int currentPlayer = 0;
	bool boombFound = false;
    bool animationWasPlayed = false;
	List<Player> players;
    ScrollSnap snap;

    public Vector2 vel;

	// Use this for initialization
	void Start () {
		players = FindObjectOfType<PlayersManager>().GetPlayers();
        System.Random rand = new System.Random();
        players = players.OrderBy(item => rand.Next()).ToList();

        for (int i = 0; i < 3; ++i)
        {
            foreach (var player in players)
            {
                GameObject template = Instantiate(playerFieldTemplate, listOfPlayers);
                template.SetActive(true);
                FillPlayerTemplate(player, template);
            }
        }
        snap = listOfPlayers.GetComponentInParent<ScrollSnap>();


        FillBombFields();
        turnInProgress = false;
	}


    private void FillBombFields()
    {
        bombsFields = new List<BombField>();
        foreach (RectTransform bombField in bombsFieldsHandler)
        {
            bombsFields.Add(bombField.GetComponent<BombField>());
        }
        Debug.Log("Ilośc pól minowych" + bombsFields.Count);
        int bombPlace = Random.Range(0, bombsFields.Count);
        bombsFields[bombPlace].hasBomb = true;
        Debug.Log("Bomb is under " + (bombPlace + 1));
    }

    float time = 0f;

	// Update is called once per frame
	void Update () {
        Debug.Log("Current page = " + snap.CurrentPage());   
		if (!boombFound)
		{
            if(!animationWasPlayed)
            {
                if(time < 0.1f) // I have yo make it better
                {
                    time += Time.deltaTime;
                }else
                {
                    PlayChoosingPlayerAnimation();
                    time = 0f;
                }
            }else
            {
                if (!turnInProgress)
                {
                    SetTurn(true);
                }
            }
		}
		else
		{
			DisactivateButtons();
			ShowLooser();
		}
	}

    void PlayChoosingPlayerAnimation()
    {
        Debug.Log("Animation started");


        if(snap.CurrentPage() < players.Count * 2 - 1)
        {
            snap.NextScreen();
        }
        else
        {
            animationWasPlayed = true;
            SetTurn(false);
            Debug.Log("Animation ended - current page:" + snap.CurrentPage());
        }
    }

    void SetTurn(bool changePlayer)
    {
        if(changePlayer)
        {
            snap.NextScreen();
            turnInProgress = true;
            i = ++i % players.Count;
            if (i == 0)
            {
                snap.ChangePage(snap.CurrentPage() - players.Count - 1);
                Debug.Log("Current page next turn " + snap.CurrentPage());
            }
        } else
        {
            turnInProgress = true;
        }

    }


	void DisactivateButtons()
	{
		foreach (BombField button in bombsFields)
		{
			button.GetComponent<Button>().interactable = false;
		}
	}
	
	void FillPlayerTemplate(Player player, GameObject template)
	{
        template.GetComponentInChildren<Text>().text = player.playerName;
        Image avatar = template.GetComponentInChildren<Image>();
		if (player.sex == "woman")
		{
            avatar.sprite = womanSprite;
		} else
		{
            avatar.sprite = manSprite;
		}
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

    int i = 0;

	public void EndGame()
	{
		turnInProgress = false;
		boombFound = true;
	}

	public void EndTurn()
	{
		currentPlayer = ++currentPlayer % players.Count;
		//FillPlayerTemplate(players[currentPlayer]);
		Debug.Log("Next Player: " + players[currentPlayer].playerName);
		turnInProgress = false;
	}
}
