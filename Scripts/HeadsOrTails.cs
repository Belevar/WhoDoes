using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class HeadsOrTails : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private Sprite tails;

    bool swipeStarted = false;


    public Sprite heads;
    public Text firstPlayerName;
    public Text secondPlayerName;
    public Image firstPlayerCoin;
    public Image secondPlayerCoin;
    public int spinnSpeed;
    public Sprite womanSprite;
    public Button changeSidesButton;
    
    bool wasThrown = false;
    bool isSpinning = false;

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
        lastTransform = Mathf.Abs(transform.eulerAngles.y);
	}

    bool coinSideWasChanged = true;
    bool  coinSideWasChanged2 = true;
    bool areHeads = false;

    int degreesToSpin = 0;
    int currentSpin = 0;
    float lastTransform;


	// Update is called once per frame
	void Update () {
        if (wasThrown)
        {
            #region oldSwipe
            /*if (currentSpinnTime < spinnBootleTime)
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
                 }
                 else
                 {
                     Debug.Log("Koniec obracania!");
                     currentSpinnTime = 0f;
                     wasThrown = false;
                 }*/
            #endregion
            #region newSwipe
            
            if(!isSpinning)
            {
                degreesToSpin = (int)spinnBootleTime * 180 * 3;
                int randomizer = Random.Range(0, 2);
                if (randomizer == 1)
                {
                    Debug.Log("Randomizer = 1");
                    degreesToSpin += 180;
                }
                else
                {
                    Debug.Log("Randomizer = 0");
                }
                isSpinning = true;
                changeSidesButton.interactable = false;
            }
            
            if (currentSpin < degreesToSpin)
            {
                currentSpinnTime += Time.deltaTime;
                int speed = rotateSpeed;
                if(currentSpin != 0)
                {
                    Debug.Log("Current spin =" + currentSpin);
                    Debug.Log("Current spin / 360 =" +  currentSpin / 360);
                    speed = rotateSpeed - currentSpin / 360;
                    //speed = 1;
                    Debug.Log("ROTATE SPEED" + rotateSpeed);
                }
                currentSpin += speed;
                
                if(throwDirection == Vector3.up)
                {
                    Debug.Log("THROW RIGHT");
                    throwToRightSide();
                }else if(throwDirection == Vector3.down)
                {
                    Debug.Log("THROW Left");
                    throwToLeftSide();
                } else
                {

                    Debug.LogError("NO THORW SIDE");
                }

                transform.Rotate(throwDirection, speed);
            }
            else
            {
                Debug.Log("Koniec obracania!");
                transform.rotation = new Quaternion(0, 0, 0, 0);
                currentSpin = 0;
                wasThrown = false;
                isSpinning = false;
            }

            #endregion
        }
        else
        {
            //swipeFingerOnScreen();
        }

	}


    void throwToRightSide()
    {
        if (Mathf.Abs(transform.eulerAngles.y) > 90 && Mathf.Abs(transform.eulerAngles.y) < 180)
        {
            Debug.Log("1: " + Mathf.Abs(transform.eulerAngles.y));
            if (coinSideWasChanged)
            {
                coinSideWasChanged = false;
                changeCoinSides();
            }
            coinSideWasChanged2 = true;
        }
        if (Mathf.Abs(transform.eulerAngles.y) > 270 && Mathf.Abs(transform.eulerAngles.y) < 360)
        {
            Debug.Log("2: " + Mathf.Abs(transform.eulerAngles.y));
            if (coinSideWasChanged2)
            {
                changeCoinSides();
                coinSideWasChanged2 = false;
            }
            coinSideWasChanged = true;
        }
    }

    void throwToLeftSide()
    {
        if (Mathf.Abs(transform.eulerAngles.y) > 90 && Mathf.Abs(transform.eulerAngles.y) < 0)
        {
            Debug.Log("1: " + Mathf.Abs(transform.eulerAngles.y));
            if (coinSideWasChanged)
            {
                coinSideWasChanged = false;
                changeCoinSides();
            }
            coinSideWasChanged2 = true;
        }
        if (Mathf.Abs(transform.eulerAngles.y) < 270 && Mathf.Abs(transform.eulerAngles.y) > 180)
        {
            Debug.Log("2: " + Mathf.Abs(transform.eulerAngles.y));
            if (coinSideWasChanged2)
            {
                changeCoinSides();
                coinSideWasChanged2 = false;
            }
            coinSideWasChanged = true;
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

    void assignPlayers()
    {
        List<Player> players = FindObjectOfType<PlayersManager>().GetPlayers();
        if(players[0].sex == "woman")
        {
            firstPlayerName.GetComponentInChildren<Image>().sprite = womanSprite;
        }
        if (players[1].sex == "woman")
        {
            secondPlayerName.GetComponentInChildren<Image>().sprite = womanSprite;
        }
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
            Debug.Log("SWIPE UP");
        }
        else
        {
            throwDirection = Vector3.down;
            Debug.Log("SWIPE DOWN");
        }
        length = final.magnitude;
        spinnBootleTime = swipeTime * length;
        if (spinnBootleTime > 15)
        {
            spinnBootleTime = 15;
        }
        degreesToSpin = (int)spinnBootleTime * 180 * 3;
        Debug.Log(spinnBootleTime);
        rotateSpeed = degreesToSpin / 360 + 2;
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
        
        if (Input.GetMouseButtonUp(0) && swipeTime > 0.25f)
        {
            ThrowCoin();
        } else if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Swipe TIme: " + swipeTime);
            swipeTime = 0f;
        }
        
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        swipeTime = 0.0f;
        startpos = Input.mousePosition;
        endpos = Vector2.zero;
        Debug.Log("BEGIN DRAG");
    }

    public void OnDrag(PointerEventData eventData)
    {
        swipeTime += Time.deltaTime;
        Debug.Log("DRAG IN PROGRESS");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        wasThrown = true;
        shouldSpinn = true;
        endpos = Input.mousePosition;
        final = endpos - startpos;
        if (startpos.y < endpos.y)
        {
            throwDirection = Vector3.up;
            Debug.Log("SWIPE UP");
        }
        else
        {
            throwDirection = Vector3.down;
            Debug.Log("SWIPE DOWN");
        }
        length = final.magnitude;
        spinnBootleTime = swipeTime * length;
        if (spinnBootleTime > 15)
        {
            spinnBootleTime = 15;
        }
        degreesToSpin = (int)spinnBootleTime * 180 * 3;
        Debug.Log(spinnBootleTime);
        rotateSpeed = degreesToSpin / 360 + 2;
        swipeTime = 0f;
            Debug.Log("DRAG END");
    }
}
