using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelToolTIp : MonoBehaviour {

    public GameObject tooltip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void disactivatePanel()
    {
        tooltip.SetActive(false);
        Debug.LogError("Implement panel to hide forever!");
    }
}
