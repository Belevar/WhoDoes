using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class SwipeApperanceController : MonoBehaviour {

    public GameObject firstAppear, secondAppear, thirdAppear, buttons;
    public HorizontalScrollSnap slider;
    Vector3 firstOrginalPos, secondOrginalPos, thirdOrginalPos, buttonsPos;
    bool isPageSelected = false;
    bool shouldMove = false;
    float lerpSpeed = 1f;
    bool appeard = false;

    void Awake()
    {
        firstOrginalPos = firstAppear.transform.localPosition;
        secondOrginalPos = secondAppear.transform.localPosition;
        thirdOrginalPos = thirdAppear.transform.localPosition;
        buttonsPos = buttons.transform.localPosition;
    }

    int pageIndex = -1;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.parent.childCount; i++)
        { 
            if (transform.parent.GetChild(i) == transform)  {
                pageIndex = i;
                break;
            }
        }
        Debug.Log("Page index" + pageIndex);
	}
	
	// Update is called once per frame
	void Update () {
        if (slider.CurrentPageForAnimation == pageIndex && !appeard) 
        {
            if(!shouldMove)
            {
                MoveElementsToStartingPoint();
            }
            if(shouldMove)
            {
                MoveElementsToDestination();
            }
        }else if (slider.CurrentPageForAnimation != pageIndex)
        {
            setOrginalPositionsOfElements();
            appeard = false;
            //shouldMove = false;
        }
	}

    void MoveElementsToStartingPoint()
    {
        Debug.Log("Move Elements to Starting Point");
        firstAppear.transform.localPosition = firstAppear.transform.localPosition + new Vector3(500f, 0f, 0);
        secondAppear.transform.localPosition = secondAppear.transform.localPosition + new Vector3(-700f, 0f, 0);
        thirdAppear.transform.localPosition = thirdAppear.transform.localPosition + new Vector3(1200f, 0f, 0);
        buttons.transform.localPosition = buttons.transform.localPosition + new Vector3(-1200f, 0f, 0);
        shouldMove = true;
    }

    void MoveElementsToDestination()
    {
        firstAppear.transform.localPosition = Vector3.Lerp(firstAppear.transform.localPosition, firstOrginalPos, lerpSpeed * 2 * Time.deltaTime);
        secondAppear.transform.localPosition = Vector3.Lerp(secondAppear.transform.localPosition, secondOrginalPos, lerpSpeed * 2 * Time.deltaTime);
        thirdAppear.transform.localPosition = Vector3.Lerp(thirdAppear.transform.localPosition, thirdOrginalPos, lerpSpeed * 2 * Time.deltaTime);
        buttons.transform.localPosition = Vector3.Lerp(buttons.transform.localPosition, buttonsPos, lerpSpeed * 2 * Time.deltaTime);
        if (Mathf.Abs(thirdAppear.transform.localPosition.x - thirdOrginalPos.x) <= 1f)
        {
            appeard = true;
            setOrginalPositionsOfElements();
        }
    }

    void setOrginalPositionsOfElements()
    {
        shouldMove = false;
        Debug.Log("Set orginal pos");
        firstAppear.transform.localPosition = firstOrginalPos;
        secondAppear.transform.localPosition = secondOrginalPos;
        thirdAppear.transform.localPosition = thirdOrginalPos;
        buttons.transform.localPosition = buttonsPos;
    }

    public void turnOffMovement()
    {
        Debug.Log("Should move = false");
        shouldMove = false;
    }
}
