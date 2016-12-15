using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerGuessNumber : MonoBehaviour
{

    public Text playerName;
    public Text guessText;
    public Image avatar;
    public Sprite womanImage;
    private int guess = 0;

    public void fillPlayerTemplate(string name, string sex)
    {
        gameObject.SetActive(true);
        playerName.text = name;
        assignCorrectAvatar(sex);
    }


    public void assignCorrectAvatar(string sex)
    {
        if (sex == "woman")
        {
            avatar.sprite = womanImage;
        }
    }

    public void greyOutPlayer()
    {
        Debug.Log("Grey out " + name);
        GetComponent<CanvasRenderer>().SetColor(Color.gray);
        avatar.GetComponent<CanvasRenderer>().SetColor(Color.gray);
    }

    public void colorPlayer()
    {
        Debug.Log("color full " + name);
        GetComponent<CanvasRenderer>().SetColor(Color.white);
        avatar.GetComponent<CanvasRenderer>().SetColor(Color.white);
    }
}
