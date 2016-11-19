using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text[] buttonList;
    public Transform buttonsPlaceholder;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public PlayerCrossAndCircle playerCircle;
    public PlayerCrossAndCircle playerCross;
    public Button changeSidesButton;
    public Text points;

    private string playerSide;
    public  Sprite defaultSprite;
    private int moveCount;
    bool gameOver = false;
    int gamesCount = 0;
    int maxGames = 2;
    

    void Awake()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
    }

	// Use this for initialization
	void Start () {
        int i = 0;
        buttonList = new Text[9];
        foreach (Transform button in buttonsPlaceholder)
        {
            buttonList[i] = button.GetComponentInChildren<Text>();
            buttonList[i++].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
        ChangeSides();
	}
	
    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        changeSidesButton.interactable = false;
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver();
        }
        if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver();
        }
      
        if (moveCount >= 9 && !gameOver)
        {
//            SetGameOverText("It's a draw!");
            gameOver = true;
        }

        if (!gameOver)
        {
            ChangeSides();
        }else
        {
            if (gamesCount++ < maxGames)
            {
                RestartGame();
            }
            else
            {
                ShowWinner();
            }
        }
    }

    void ShowWinner()
    {
        if (playerCross.wins < playerCircle.wins)
        {
            SetGameOverText(playerCircle.playerName.text + " Wins!");
        }
        else if (playerCross.wins > playerCircle.wins)
        {
            SetGameOverText(playerCross.playerName.text + " Wins!");
        }
        else
        {
            SetGameOverText("It's a draw");
        }
                
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        Debug.Log("Player SIde" + playerSide);
        if(playerSide == "O")
        {
            playerCross.greyOutPlayer();
            playerCircle.colorPlayer();
        }else
        {
            playerCross.colorPlayer();
            playerCircle.greyOutPlayer(); 
        }

    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        gameOver = true;
        ChooseRoundWiner();
    }

    void ChooseRoundWiner()
    {
        if(playerSide == "X")
        {
            Debug.Log(++playerCross.wins);
        }else
        {
            Debug.Log(++playerCircle.wins);
        }
        points.text = playerCircle.wins + "\n-\n" + playerCross.wins;
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }


    void RestartGame()
    {
        playerSide = "X";
        ChangeSides();
        moveCount = 0;
        gameOver = false;
        gameOverPanel.SetActive(false);
        SetBoardInteractable(true);
        changeSidesButton.interactable = true;

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
            buttonList[i].GetComponentInParent<Image>().sprite = defaultSprite;
        }
    }

    void SetBoardInteractable (bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void ChangePlayersNames()
    {
        string name = playerCircle.playerName.text;
        Sprite avatar = playerCircle.avatar.sprite;
        int wins = playerCircle.wins;
        playerCircle.playerName.text = playerCross.playerName.text;
        playerCircle.avatar.sprite = playerCross.avatar.sprite;
        playerCircle.wins = playerCross.wins;

        playerCross.playerName.text = name;
        playerCross.avatar.sprite = avatar;
        playerCross.wins = wins;
        points.text = playerCircle.wins + "\n-\n" + playerCross.wins;
    }
}
