using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EndGame : MonoBehaviour {

    GameResult gameResult;
    public Text text;
    public Image avatar;
    public Sprite woman, man;

	// Use this for initialization
	void Start () {
        gameResult = FindObjectOfType<GameResult>();
	    Player looser = gameResult.getLooser();
        setAvatar(looser.sex);
        setEndText(gameResult.text, looser.playerName);
        GetComponent<Image>().sprite = gameResult.sprite;
    }
	
    void setEndText(string message, string playerName)
    {
        string fullMess = playerName + '\n' + message.Substring(message.IndexOf(' ')).ToLower() ;
        text.text = fullMess;
    }
    
    void setAvatar(string sex)
    {
        if(sex == "man")
        {
            avatar.sprite = man;
        }else
        {
            avatar.sprite = woman;
        }
    }


}
