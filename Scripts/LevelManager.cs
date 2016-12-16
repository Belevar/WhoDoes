using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class LevelManager : MonoBehaviour {

    //enum FOR_WHAT_PLAYERS_PLAY { WALK_DOG, DISHES, LAUNDRY, VACUM ,CUSTOM}
    //FOR_WHAT_PLAYERS_PLAY gamePurpose;


    GameResult gameResult;

	// Use this for initialization
	void Start () {
        gameResult = FindObjectOfType<GameResult>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 0)
        {
            gameResult = FindObjectOfType<GameResult>();
            gameResult.SaveGamePurpose();
        }
        SceneManager.LoadScene(currentScene + 1);
    }

   

    public void LoadPreviousScene()
    {
        if(SceneManager.GetActiveScene().name == "choose_game")
        {
            Debug.Log("Destroy Players Manager");
            Destroy(FindObjectOfType<PlayersManager>().gameObject);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}


