using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIVisibilityManager : MonoBehaviour {


    public GameObject[] objectToHide;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void  HideGUIObjects()
    {
        foreach (var item in objectToHide)
        {
            item.SetActive(false);
        }
    }


    public void ShowGUIObjects()
    {
        foreach (var item in objectToHide)
        {
            item.SetActive(true);
        }
    }
}
