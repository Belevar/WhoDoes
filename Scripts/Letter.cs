using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Letter : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    bool thereIsPlaceHolderClose = false;
    GameObject closePlaceholder = null;
    public RectTransform [] placehodersForLetters;
    public float rangeSqr = 2500f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SearchForClosePlaceholder();
    }

    void SearchForClosePlaceholder()
    {
          
        foreach (RectTransform place in placehodersForLetters){
            Debug.Log("Placeholder pos : " + place.position);
            Debug.Log("Letter pos:" + Input.mousePosition);
            float distanceSqr = (Input.mousePosition - place.position).sqrMagnitude;
            Debug.Log("Distance between objects: " + distanceSqr);
            if(distanceSqr < rangeSqr)
            {
                closePlaceholder = place.gameObject;
                thereIsPlaceHolderClose = true;
                Debug.Log("I HAVE PLACEHOLDER!");
                return;
            }
        }
        thereIsPlaceHolderClose = false;
    }

    Vector3 orginalTransform;
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBEGIN DRAG");
        orginalTransform = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        if(thereIsPlaceHolderClose)
        {
            SnapToPlaceHolder(closePlaceholder.transform.position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag");
        //this.transform.position = orginalTransform;
    }

    void SnapToPlaceHolder(Vector3 placehoderPos)
    {
        transform.position = placehoderPos;
    }




}
