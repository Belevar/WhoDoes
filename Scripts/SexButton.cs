using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SexButton : MonoBehaviour {

    Sprite manSprite;
    public Sprite womanSprite;
    Image buttonImage;
    string currentSex;

    void Awake()
    {
        Debug.Log("Awake SEXBUTTOn");
        buttonImage = GetComponent<Button>().image;
        manSprite = buttonImage.sprite;
        currentSex = "man";
    }

	// Use this for initialization
	void Start () {
        
	}

    public void ChangeSex()
    {
        if(currentSex == "man")
        {
            currentSex = "woman";
            buttonImage.sprite = womanSprite;
        }
        else
        {
            currentSex = "man";
            buttonImage.sprite = manSprite;
        }
    }

    public void SetSex(string sex)
    {
        if (sex == "woman")
        {
            currentSex = "woman";
            buttonImage.sprite = womanSprite;
        }
    }
	

    public string GetSex()
    {
        return currentSex;
    }
}
