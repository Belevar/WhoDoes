using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerFieldTouchEvent : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    float swipeTime = 0.0f;
    float swipeStartX = 0.0f;
    float swipeEndX = 0.0f;
    public PlayersFields playersFields;
    public float swipeOutPositionTrigger = 160;

    enum SwipeState { MOVE, NO_MOVEMENT, BACK_TO_ORGINAL_POS, MOVE_UP}

    SwipeState swipeState;
    Vector3 posToMove;
    Transform parentObject;

    bool repositionPlayersFieldStarted = false;
    int indexInChildHierarchy = -1;

    void Awake()
    {
        parentObject = transform.parent;
        swipeState = SwipeState.NO_MOVEMENT;
        if (playersFields == null)
        {
            playersFields = FindObjectOfType<PlayersFields>();
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (swipeState == SwipeState.MOVE)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, posToMove, 3f * Time.deltaTime);
            if(!repositionPlayersFieldStarted)
            {
              //  indexInChildHierarchy = findIndexOfGameObject();
                for (int i = indexInChildHierarchy ; i < parentObject.childCount; i++) 
                {
                    parentObject.GetChild(i).GetComponent<PlayerFieldTouchEvent>().MoveUP();
                }
                //this.gameObject.transform.SetParent(null);
                repositionPlayersFieldStarted = true;
            }
            if(Mathf.Abs(transform.localPosition.x - posToMove.x) <= 40f)
            {
                playersFields.DeletePlayer(this.transform);
                repositionPlayersFieldStarted = false;
                transform.localPosition = posToMove;
                swipeState = SwipeState.NO_MOVEMENT;
            }
        }
        else if (swipeState == SwipeState.BACK_TO_ORGINAL_POS)
        {
            Debug.Log("Back t oOrginal Pos");
            transform.localPosition = Vector3.Lerp(transform.localPosition, posToMove, 3f * Time.deltaTime);
            if (Mathf.Abs(transform.localPosition.x - posToMove.x) <= 0.015f)
            {
                Debug.Log("Back t oOrginal Pos end");
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


    public void moveNewPlayer()
    {
        swipeState = SwipeState.BACK_TO_ORGINAL_POS;
        this.posToMove = transform.localPosition;
        posToMove.x = 0f;
        Debug.Log("End move new Player pos: " + this.posToMove);
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
        if(swipeState == SwipeState.MOVE_UP || swipeState == SwipeState.BACK_TO_ORGINAL_POS)
        {
            posToMove = new Vector3(0f, posToMove.y + playersFields.moveUpSHIT, 0f);
        }
        else
        {
            posToMove = new Vector3(0f, transform.localPosition.y + playersFields.moveUpSHIT, 0f);
        }
        swipeState = SwipeState.MOVE_UP;
    }

    #region Interfaces
    public void OnBeginDrag(PointerEventData eventData)
    {
        swipeTime = 0.0f;
        swipeStartX = Input.mousePosition.x;
        swipeEndX = 0.0f;
        //swipeState = SwipeState.NO_MOVEMENT;
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
            if (swipeEndX > swipeOutPositionTrigger)
            {
                posToMove = new Vector3(640, transform.localPosition.y, transform.localPosition.z);
                Debug.Log("Right");
                swipeState = SwipeState.MOVE;
                indexInChildHierarchy = findIndexOfGameObject();
                this.gameObject.transform.SetParent(null);
            } else if(swipeEndX < -swipeOutPositionTrigger)
            {
                Debug.Log("LEFT");
                posToMove = new Vector3(-640, transform.localPosition.y, transform.localPosition.z);
                swipeState = SwipeState.MOVE;
                indexInChildHierarchy = findIndexOfGameObject();
                this.gameObject.transform.SetParent(null);
            } else
            {
                if(swipeState == SwipeState.MOVE_UP)
                {
                    Debug.Log("Back to orgin pos - change only X");
                    posToMove.x = 0;
                }else
                {
                    posToMove = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
                    Debug.Log("Back to orgin pos - change all");
                }
                swipeState = SwipeState.BACK_TO_ORGINAL_POS;
            }
            Debug.Log("DRAG END");
        }
    }

     
    #endregion


 }
