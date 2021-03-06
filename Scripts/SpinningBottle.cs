﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpinningBottle : MonoBehaviour
{

    public RectTransform spinningBottle;
    private float length = 0;
    private Vector3 final;
    public Vector3 startpos;
    public Vector3 endpos;

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

    private bool madeFirstCycle;


    Vector3 swipeDirection = Vector3.zero;
    void Start()
    {
        madeFirstCycle = false;
        gameOver = false;
        Vector3 center = transform.position;
        playersManager = FindObjectOfType<PlayersManager>();
        players = playersManager.GetPlayers();
        for (int i = 0; i < players.Count; i++)
        {
            int a = i * 360 / players.Count;
            Vector3 pos = RandomCircle(center, 200.0f, a);
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
            if (!madeFirstCycle)
            {
                rotateFirstCycle();
            }

            if (currentSpin < degreesToSpin && madeFirstCycle)
            {
                int speed = rotateSpeed;
                if (currentSpin != 0)
                {
                  //  Debug.Log("Current spin =" + currentSpin);
                  //  Debug.Log("Current spin / 360 =" + currentSpin / 360);
                    speed = rotateSpeed - currentSpin / 360;
                  //  Debug.Log("ROTATE SPEED" + rotateSpeed);
                }
                currentSpin += speed;
                spinningBottle.Rotate(swipeDirection, speed);
            }
            else if (madeFirstCycle)
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
    }


    void swipeFingerOnScreen()
    {
        if (Input.GetMouseButton(0))
        {
            startpos = Input.mousePosition;
            swipeTime += Time.deltaTime;

            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if (Input.GetMouseButtonUp(0))
        {
            shouldSpinn = true;
            endpos = Input.mousePosition;
            final = endpos - startpos;

            Debug.Log("startpos pos:" + startpos);
            Debug.Log("endpos pos:" + endpos);
            Debug.Log("Final pos:" + final);
            if (startpos.y < endpos.y)
            {
                if (startpos.x > endpos.x)
                {  //swipeDirection = Vector3.back;
                    swipeDirection = Vector3.forward;
                    Debug.Log("Forward! 1");
                    degreesForFirstCycle = 360 - (int)Mathf.Abs(transform.rotation.eulerAngles.z) + 45;//to set start point on first player
                    
                }
                else
                {
                    swipeDirection = Vector3.back;
                    Debug.Log("Back 1");
                    degreesForFirstCycle = (int)Mathf.Abs(transform.rotation.eulerAngles.z) - 45;// to set start point on first player
                }
            }
            else
            {
                if(startpos.x < endpos.x)
                {
                   
                    swipeDirection = Vector3.back;
                    Debug.Log("Back 2");
                    degreesForFirstCycle = (int)Mathf.Abs(transform.rotation.eulerAngles.z) - 45;// to set start point on first player
                    //swipeDirection = Vector3.forward;
                }
                else
                {
                    swipeDirection = Vector3.forward;
                    Debug.Log("Forward! 2");
                    degreesForFirstCycle = 360 - (int)Mathf.Abs(transform.rotation.eulerAngles.z) + 45;//to set start point on first player
                }
                
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
           // Debug.Log("Current rotation " + transform.rotation.eulerAngles.z);
          //  Debug.Log("Degrees to normal pos " + Mathf.Abs(transform.rotation.eulerAngles.z));

            degreesToSpin = (int)spinnBootleTime * 360 / playersManager.GetPlayers().Count * 10;
            int randomizer = Random.Range(0, playersManager.GetPlayers().Count + 1);
            degreesToSpin += randomizer * 360 / playersManager.GetPlayers().Count;
           // Debug.Log("Degres to spinn =" + degreesToSpin);
            rotateSpeed = degreesToSpin / 360 + 2;
        }
    }

    int degreesForFirstCycle = 0;

    void rotateFirstCycle()
    {

        if (currentSpin < degreesForFirstCycle)
        {
           // Debug.Log("Current spin first cycle: " + currentSpin);
            int speed = 15;
            currentSpin += speed;
            spinningBottle.Rotate(swipeDirection, speed);
        }
        else
        {
           // Debug.Log("After first circle " + transform.rotation.eulerAngles);
            currentSpin = currentSpin - degreesForFirstCycle;
            madeFirstCycle = true;
        }
    }



    public enum Swipe { None, Up, Down, Left, Right };

    public static Swipe swipeDirection2;
}


