using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TickingBomb : MonoBehaviour {


    float timeTillExplosion;
    public float timeMultiplayer = 30;
    bool bombPlanted = false;
    bool endGame = false;
    public Text infoText;
    public Text loseMessage;
    GameObject bombGraphic;
    float timePassed = 0.0f;

	// Use this for initialization
	void Start () {
        int players = FindObjectOfType<PlayersManager>().GetPlayers().Count;
        bombGraphic = transform.GetChild(0).gameObject;
        timeTillExplosion = Random.Range(1, players);
        timeTillExplosion *= timeMultiplayer;
       // Debug.Log("Time till Explosion " + timeTillExplosion);
    }
	
	// Update is called once per frame
	void Update () {

        if(bombPlanted && !endGame)
        {
            if(timePassed < timeTillExplosion)
            {
                timePassed += Time.deltaTime;
               // Debug.Log(timePassed);
            }else
            {
                endGame = true;
                bombGraphic.SetActive(false);
                loseMessage.enabled = true;
            }
        }

        if(Input.GetMouseButtonDown(0) && !endGame)
        {
            bombPlanted = true;
            //infoText.enabled = false;
            infoText.text = "ANIMACJA BĘDZIE I COŚ!";
            //start Animation
        }
	}
}
