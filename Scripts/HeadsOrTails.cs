using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeadsOrTails : MonoBehaviour {

    private Sprite tails;

    bool swipeStarted = false;


    public Sprite heads;
    public Text firstPlayerName;
    public Text secondPlayerName;
    public Image firstPlayerCoin;
    public Image secondPlayerCoin;
    
    bool wasThrown = false;


    private float length = 0;
    private bool SW = false;
    private Vector3 final;
    private Vector3 startpos;
    private Vector3 endpos;

    private float swipeTime = 0.0f;
    float spinnBootleTime;
    float currentSpinnTime = 0f;
    bool shouldSpinn = false;
    public int rotateSpeed = 5;
    Vector3 throwDirection = Vector3.up;
    void Awake()
    {
        tails = GetComponent<Image>().sprite;
    }

	// Use this for initialization
	void Start () {
        assignPlayers();
	}

    bool coinSideWasChanged = true;
    bool  coinSideWasChanged2 = true;
    bool areHeads = false;
	// Update is called once per frame
	void Update () {
        if (wasThrown)
        {
            if (currentSpinnTime < spinnBootleTime)
            {
                currentSpinnTime += Time.deltaTime;
                Debug.Log("ROTATION " + Mathf.Abs(transform.eulerAngles.y));
                if (Mathf.Abs(transform.eulerAngles.y) > 90 && Mathf.Abs(transform.eulerAngles.y) < 180)
                {

                    if (coinSideWasChanged)
                    {
                        coinSideWasChanged = false;
                        changeCoinSides();
                    }
                    coinSideWasChanged2 = true;
                }
                if (Mathf.Abs(transform.eulerAngles.y) > 270 && Mathf.Abs(transform.eulerAngles.y) < 360)
                {
                    if (coinSideWasChanged2)
                    {
                        changeCoinSides();
                        coinSideWasChanged2 = false;
                    }
                    coinSideWasChanged = true;
                }
                transform.Rotate(throwDirection,  (spinnBootleTime - currentSpinnTime));
                //transform.Rotate()
            }
            else
            {
                Debug.Log("Koniec obracania!");
                currentSpinnTime = 0f;
                wasThrown = false;
            }
        }
        else
        {
            swipeFingerOnScreen();
        }

	}

    void changeCoinSides()
    {
        if (areHeads)
        {
            Debug.Log("TAILS!");
            GetComponent<Image>().sprite = tails;
            areHeads = false;
        }
        else
        {
            Debug.Log("HEADS!");
            areHeads = true;
            GetComponent<Image>().sprite = heads;
        }
    }

    void spinCoin()
    {

    }

    void assignPlayers()
    {
        List<Player> players = FindObjectOfType<PlayersManager>().GetPlayers();
        firstPlayerName.text = players[0].playerName;
        secondPlayerName.text = players[1].playerName;
    }

    void ThrowCoin()
    {
        wasThrown = true;
        shouldSpinn = true;
        endpos = Input.mousePosition;
        final = endpos - startpos;
        if (startpos.y < endpos.y)
        {
            throwDirection = Vector3.up;
        }
        else
        {
            throwDirection = Vector3.down;
        }
        length = final.magnitude;
        spinnBootleTime = swipeTime * length;
        if (spinnBootleTime > 15)
        {
            spinnBootleTime = 15;
        }
        Debug.Log(spinnBootleTime);
        swipeTime = 0f;
    }

    void SelectWinner(int result)
    {
        if(result == 1)
        {
            GetComponent<Image>().sprite = tails;
            Debug.Log("Player 1 is winner");
        }else
        {
            GetComponent<Image>().sprite = heads;
            Debug.Log("Player 2 is winner");
        }
    }

    public void ChangePlayerCoinSide()
    {
        Sprite temp = firstPlayerCoin.sprite;
        firstPlayerCoin.sprite = secondPlayerCoin.sprite;
        secondPlayerCoin.sprite = temp;
    }

    void swipeFingerOnScreen()
    {
        if (Input.GetMouseButton(0))
        {
            startpos = Input.mousePosition;
            swipeTime += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ThrowCoin();
        }
    }
}
