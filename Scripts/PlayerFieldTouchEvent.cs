using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerFieldTouchEvent : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    float swipeTime = 0.0f;
    float swipeStartX = 0.0f;
    float swipeEndX = 0.0f;
    public PlayersFields playersFields;

    enum SwipeState { MOVE, NO_MOVEMENT, BACK_TO_ORGINAL_POS, MOVE_UP}

    SwipeState swipeState;
    Vector3 posToMove;

    bool repositionPlayersFieldStarted = false;
    int index = -1;

	// Use this for initialization
	void Start () {
        swipeState = SwipeState.NO_MOVEMENT;
        if(playersFields == null)
        {
            playersFields = FindObjectOfType<PlayersFields>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (swipeState == SwipeState.MOVE)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posToMove, 3f * Time.deltaTime);
            if(!repositionPlayersFieldStarted)
            {
                index = findIndexOfGameObject();
                for (int i = index + 1; i < transform.parent.childCount; i++)
                {
                    transform.parent.GetChild(i).GetComponent<PlayerFieldTouchEvent>().MoveUP();
                }
                repositionPlayersFieldStarted = true;
            }
            if(Mathf.Abs(transform.localPosition.x - posToMove.x) <= 40f)
            {
                playersFields.DeletePlayer(index);
                repositionPlayersFieldStarted = false;
                transform.localPosition = posToMove;
                swipeState = SwipeState.NO_MOVEMENT;
            }
        }
        else if (swipeState == SwipeState.BACK_TO_ORGINAL_POS)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posToMove, 3f * Time.deltaTime);
            if (Mathf.Abs(transform.localPosition.x - posToMove.x) <= 0.015f)
            {
                transform.localPosition = posToMove;
                swipeState = SwipeState.NO_MOVEMENT;
            }
        }
        else if(swipeState == SwipeState.MOVE_UP)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posToMove, 2f * Time.deltaTime);
            if(Mathf.Abs(transform.localPosition.y - posToMove.y) <= 1f)
            {
                transform.localPosition = posToMove;
                swipeState = SwipeState.NO_MOVEMENT;
            }
        }
	}

    int findIndexOfGameObject()
    {
        var myself = transform;
        var parent = transform.parent;
        int thisGameObjectIndex = parent.childCount;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == myself)
            {
                thisGameObjectIndex = i;
            }
        }
        return thisGameObjectIndex;
    }


    public void MoveUP()
    {
        posToMove = new Vector3(transform.localPosition.x, transform.localPosition.y + playersFields.spaceBetweenFields, 0f);
        swipeState = SwipeState.MOVE_UP;
    }

    #region Interfaces
    public void OnBeginDrag(PointerEventData eventData)
    {
        swipeTime = 0.0f;
        swipeStartX = Input.mousePosition.x;
        swipeEndX = 0.0f;
        swipeState = SwipeState.NO_MOVEMENT;
        Debug.Log("BEGIN DRAG");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent.childCount > 2)
        {
            swipeTime += Time.deltaTime;
            transform.position = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
            Debug.Log("DRAG IN PROGRESS");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent.childCount > 2)
        {
            swipeEndX = transform.localPosition.x;
            Debug.Log("Swipe End X" + swipeEndX);
            if(swipeEndX > 210)
            {
                posToMove = new Vector3(640, transform.localPosition.y, transform.localPosition.z);
                Debug.Log("Right");
                swipeState = SwipeState.MOVE;
            } else if(swipeEndX < -210)
            {
                Debug.Log("LEFT");
                posToMove = new Vector3(-640, transform.localPosition.y, transform.localPosition.z);
                swipeState = SwipeState.MOVE;
            } else
            {
                posToMove = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
                swipeState = SwipeState.BACK_TO_ORGINAL_POS;
            }
            Debug.Log("Drag Time " + swipeTime * Mathf.Abs(swipeEndX - swipeStartX));
            Debug.Log("DRAG END");
        }
    }

     
    #endregion


 }
