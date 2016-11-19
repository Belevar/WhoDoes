using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
	
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
