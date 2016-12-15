using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpinningBottle : MonoBehaviour {

    public RectTransform spinningBottle;
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
    public GameObject playerTemplate;
    public Sprite manImage;
    PlayersManager playersManager;
    List<Player> players;
    public bool gameOver;


    Vector3 swipeDirection = Vector3.zero;
    void Start()
    {
        gameOver = false;
        Vector3 center = transform.position;
         playersManager = FindObjectOfType<PlayersManager>();
         players = playersManager.GetPlayers();
         for (int i = 0; i < players.Count; i++)
        {
            int a = i * 360 / players.Count;
            Vector3 pos = RandomCircle(center, 175.0f, a);
            GameObject player = Instantiate(playerTemplate, pos, Quaternion.identity) as GameObject;
            player.transform.SetParent(transform.parent);
            player.GetComponentInChildren<Text>().text = players[i].playerName;
            if (players[i].sex == "man")
            {
                player.GetComponent<Image>().sprite = manImage;
            }
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    int degreesToSpin;
    int currentSpin = 0;
    // Update is called once per frame
    void Update()
    {
        if (shouldSpinn)
        {
            
            
            /*if(currentSpinnTime < spinnBootleTime)
            {
                currentSpinnTime += Time.deltaTime;
                spinningBottle.Rotate(swipeDirection * (spinnBootleTime - currentSpinnTime));
            }*/
            if (currentSpin < degreesToSpin)
            {
                int speed = rotateSpeed;
                if (currentSpin != 0)
                {
                    Debug.Log("Current spin =" + currentSpin);
                    Debug.Log("Current spin / 360 =" + currentSpin / 360);
                    speed = rotateSpeed - currentSpin / 360;
                    Debug.Log("ROTATE SPEED" + rotateSpeed);
                }
                currentSpin += speed;
                spinningBottle.Rotate(swipeDirection, speed);
            }
            else
            {
                Debug.Log("Koniec obracania!");
                currentSpinnTime = 0f;
                currentSpin = 0;
                shouldSpinn = false;
                gameOver = true;
            }
        }
        else
        {
            swipeFingerOnScreen();
        }




        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("DZIAŁAM")
            final = Vector3.zero;
            length = 0;
            SW = false;
            Vector2 touchDeltaPosition = Input.GetTouch(0).position;
            startpos = new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            SW = true;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            SW = false;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            SW = false;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (SW)
            {
                Debug.Log("OBRACAM KURWA");
                Vector2 touchPosition = Input.GetTouch(0).position;
                endpos = new Vector3(touchPosition.x, 0, touchPosition.y);
                final = endpos - startpos;
                length = final.magnitude;
                spinningBottle.RotateAround(Vector3.zero, Vector3.up, length);
            }
        }
        */
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
            shouldSpinn = true;
            endpos = Input.mousePosition;
            final = endpos - startpos;
            if(startpos.y < endpos.y)
            {
                swipeDirection = Vector3.back;
            }else
            {
                swipeDirection = Vector3.forward;
            }
            length = final.magnitude;
            spinnBootleTime = swipeTime * length;
            if (spinnBootleTime > 15)
            {
                spinnBootleTime = 15;
            }
            Debug.Log(spinnBootleTime);
            swipeTime = 0f;

            //should be random chosing players in bootle;
            degreesToSpin = (int)spinnBootleTime * 360 / playersManager.GetPlayers().Count * 10;
            int randomizer = Random.Range(0, playersManager.GetPlayers().Count + 1);
            degreesToSpin += randomizer * 360 / playersManager.GetPlayers().Count;
            rotateSpeed = degreesToSpin / 360 + 2;
        }
    }


}
