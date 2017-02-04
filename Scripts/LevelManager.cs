using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class LevelManager : MonoBehaviour {

	//enum FOR_WHAT_PLAYERS_PLAY { WALK_DOG, DISHES, LAUNDRY, VACUM ,CUSTOM}
	//FOR_WHAT_PLAYERS_PLAY gamePurpose;

	public RectTransform test;

	GameResult gameResult;

	// Use this for initialization
	void Start () {
		gameResult = FindObjectOfType<GameResult>();
	}
	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                if(SceneManager.GetSceneByName("choose_game").buildIndex < SceneManager.GetActiveScene().buildIndex)
                {
                    LoadScene("choose_game");
                }
                else
                {
                    LoadPreviousScene();
                }
                
            }
        }
	}


	public void testen()
	{
		test.rect.Set(0, 0,6, 7);
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


