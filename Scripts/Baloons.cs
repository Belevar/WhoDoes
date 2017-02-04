using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Baloons : MonoBehaviour {

    public GameObject badBaloonPrefab;
    public GameObject onePointBaloonPrefab;
    public GameObject twoPointBaloonPrefab;
    public float xMin, xMax, yPos, minVelocity, maxVelocity;
    public Sprite womanSprite;
    public Sprite manSprite;
    public Text timer;
    public GameObject playerFieldTemplate;
    public Transform listOfPlayers;
    public Transform baloonsPlaceholder;

    List<Player> players;
    List<int> playersPoints;
    bool turnInProgress;
    bool nextPlayerTurn;
    bool spawnStarted;
    int currentPlayer;
    float currentTurnTime = 0f;
    float turnTime;
    bool sceneSet = false;
    GUIVisibilityManager visibilityManager;


    // Use this for initialization
    void Start()
    {
        spawnStarted = false;
        players = FindObjectOfType<PlayersManager>().GetPlayers();
        playersPoints = new List<int>();
        nextPlayerTurn = true;
        foreach (var player in players)
        {
            GameObject template = Instantiate(playerFieldTemplate, listOfPlayers);
            template.SetActive(true);
            FillPlayerTemplate(player, template);
            playersPoints.Add(0);
        }
        visibilityManager = GetComponent<GUIVisibilityManager>();
        currentPlayer = 0;
        turnTime = 10.0f;
        turnInProgress = false;
        //FillPlayerTemplate(players[currentPlayer]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer < players.Count && nextPlayerTurn)
        {
            if (!turnInProgress)
            {
                if(!sceneSet)
                {
                    SetScene();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    visibilityManager.HideGUIObjects();
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

    void FillPlayerTemplate(Player player, GameObject template)
    {
        template.GetComponentInChildren<Text>().text = player.playerName;
        Image avatar = template.GetComponentInChildren<Image>();
        if (player.sex == "woman")
        {
            avatar.sprite = womanSprite;
        }
        else
        {
            avatar.sprite = manSprite;
        }
    }


    void SetScene()
    {
        listOfPlayers.GetComponentInParent<ScrollSnap>().NextScreen();
        Debug.Log("Set Scene");
        timer.text = "Tap to start";
        sceneSet = true;
    }

    void EndTurn()
    {
        visibilityManager.ShowGUIObjects();
        spawnStarted = false;
        timer.text = "Your score: " + playersPoints[currentPlayer].ToString();
        currentTurnTime = 0f;
        currentPlayer++;
        turnInProgress = false;
        CleanScene();
        StartCoroutine(WaitBeforeNextPlayer());
        sceneSet = false;
    }

    IEnumerator WaitBeforeNextPlayer()
    {
        nextPlayerTurn = false;
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(2);
        nextPlayerTurn = true;
    }

    void CleanScene()
    {
        foreach (Transform bal in baloonsPlaceholder)
        {
            Destroy(bal.gameObject);
        }
    }

    void ShowLooser()
    {
        int worstPlayer = 0;
        int worstPlayerScore = 1000;
        for (int i = 0; i < playersPoints.Count; i++)
        {
            if (worstPlayerScore < playersPoints[i])
            {
                worstPlayer = i;
                worstPlayerScore = playersPoints[i];
            }
        }
        Debug.Log("Worst player name: " + players[worstPlayer].playerName + " score" + worstPlayerScore);
    }

    IEnumerator SpawnBadBallons()
    {
        while(currentTurnTime < turnTime)
        {
           for (int i = 0; i < Random.Range(3,7); i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), yPos - 5);
                GameObject ballon = Instantiate(badBaloonPrefab, pos, Quaternion.identity) as GameObject;
                ballon.transform.SetParent(baloonsPlaceholder);
                ballon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(minVelocity, maxVelocity));
           }
            Debug.Log("HUE HUE HUE SPAWN");
            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator SpawnOnePointBallons()
    {
        while (currentTurnTime < turnTime)
        {
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), yPos - 5);
                GameObject ballon = Instantiate(onePointBaloonPrefab, pos, Quaternion.identity) as GameObject;
                ballon.transform.SetParent(baloonsPlaceholder);
                ballon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(minVelocity, maxVelocity));
            }
            Debug.Log("HUE HUE HUE SPAWN one point");
            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator SpawnTwoPointBallons()
    {
        while (currentTurnTime < turnTime)
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMax), yPos - 5);
                GameObject ballon = Instantiate(twoPointBaloonPrefab, pos, Quaternion.identity) as GameObject;
                ballon.transform.SetParent(baloonsPlaceholder);
                ballon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(minVelocity, maxVelocity));
            }
            Debug.Log("HUE HUE HUE SPAWN 2 points");
            yield return new WaitForSeconds(1);
        }

    }

    void SpawnBallonsForTurnDuration()
    {
        if (currentTurnTime < turnTime)
        {
            currentTurnTime += Time.deltaTime;
            timer.text = (turnTime - currentTurnTime).ToString("0.00");
            if(!spawnStarted)
            {
                spawnStarted = true;
                StartCoroutine(SpawnBadBallons());
                StartCoroutine(SpawnOnePointBallons());
                StartCoroutine(SpawnTwoPointBallons());
            }
        } else
        {
            StopAllCoroutines();
            Debug.Log("Player Points = " + playersPoints[currentPlayer]);
            currentTurnTime = 0;
            EndTurn();
        }
    }

    public void AddPointForPlayer(int pointValue)
    {
        playersPoints[currentPlayer] += pointValue;
    }

    public void LosePoint(int pointValue)
    {
        playersPoints[currentPlayer] -= 1;
    }
}
