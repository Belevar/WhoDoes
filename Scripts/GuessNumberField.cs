using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GuessNumberField : MonoBehaviour
{

    public Button button;
    public Text buttonText;
    public Sprite[] images;

    public GuessNumber gameController;

    public void GetPlayerGuess()
    {
        if(gameController.turnInProgress)
        {
            GetComponent<Button>().interactable = false;
            int guess = int.Parse(GetComponent<Button>().GetComponentInChildren<Text>().text);
            gameController.GetPlayerGuess(guess);
        }
    }

}
