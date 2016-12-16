using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class GameResult : MonoBehaviour
{
    GameObject pagesPlaceholder;
    HorizontalScrollSnap scroll;
    public Player looser;

    // Use this for initialization
    public Sprite sprite;
    public string text;
    static GameResult instance = null;

    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Destroy Players Manager on AWAKE");
            Destroy(gameObject);
        }
        else
        {
            scroll = FindObjectOfType<HorizontalScrollSnap>();
            looser = new Player();
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveGamePurpose()
    {
        if (scroll == null)
        {
            scroll = FindObjectOfType<HorizontalScrollSnap>();
        }
        if (pagesPlaceholder == null)
        {
            pagesPlaceholder = scroll.transform.GetChild(0).gameObject;
        }

        int index = scroll.CurrentPageForAnimation;
        GameObject currentPage = pagesPlaceholder.GetComponent<RectTransform>().GetChild(index).gameObject;
        Image graphic = currentPage.transform.GetChild(1).GetComponent<Image>();
        if (graphic != null)
        {
            if (graphic.sprite != null)
            {
                Debug.Log("MOMY GRAFIKE");
                sprite = graphic.sprite;
            }
        }
        text = currentPage.GetComponentInChildren<Text>().text;
        if (text == "")
        {
            text = currentPage.GetComponentInChildren<InputField>().text;
        }

    }

    public void setLooser(string name, string sex)
    {
        looser.playerName = name;
        looser.sex = sex;
    }

    public Player getLooser()
    {
        return looser;
    }
}
