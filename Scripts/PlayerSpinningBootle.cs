using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpinningBootle : MonoBehaviour {

    SpinningBottle bootle;

	// Use this for initialization
	void Start () {
        bootle = FindObjectOfType<SpinningBottle>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("ON ENTER");
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(bootle.gameOver)
        {
            Debug.Log("PLAYER " + GetComponentInChildren<Text>().text + " LOST");
        }
        Debug.Log("ON STAY");
    }

}
