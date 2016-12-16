using UnityEngine;
using System.Collections;

public class Baloon : MonoBehaviour {


    Baloons gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<Baloons>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnMouseDown()
    {
        gameController.AddPointForPlayer();
        Destroy(gameObject);
    }
}
