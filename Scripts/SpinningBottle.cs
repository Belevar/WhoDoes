using UnityEngine;
using System.Collections;

public class SpinningBottle : MonoBehaviour {

    public RectTransform spinningBottle;
    private float length = 0;
    private bool SW = false;
    private Vector3 final;
    private Vector3 startpos;
    private Vector3 endpos;

    private float swipeTime = 0.0f;
    float spinnBootleTime;
    float currentSpinnTime = 0f;
    bool shouldSpinn = false;
    public int rotateSpeed = 5;
    public GameObject playerTemplate;


    Vector3 swipeDirection = Vector3.zero;
    void Start()
    {
        Vector3 center = transform.position;
        PlayersManager playersManager = FindObjectOfType<PlayersManager>();
        for (int i = 0; i < playersManager.GetPlayers().Count; i++)
        {
            int a = i * 360 / playersManager.GetPlayers().Count;
            Vector3 pos = RandomCircle(center, 150.0f, a);
            GameObject player = Instantiate(playerTemplate, pos, Quaternion.identity) as GameObject;
            player.transform.SetParent(transform.parent);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpinn)
        {
            if(currentSpinnTime < spinnBootleTime)
            {
                currentSpinnTime += Time.deltaTime;
                spinningBottle.Rotate(swipeDirection * (spinnBootleTime - currentSpinnTime));
            }
            else
            {
                Debug.Log("Koniec obracania!");
                currentSpinnTime = 0f;
                shouldSpinn = false;
            }
        }
        else
        {
            swipeFingerOnScreen();
        }




        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("DZIAŁAM")
            final = Vector3.zero;
            length = 0;
            SW = false;
            Vector2 touchDeltaPosition = Input.GetTouch(0).position;
            startpos = new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            SW = true;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            SW = false;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            SW = false;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (SW)
            {
                Debug.Log("OBRACAM KURWA");
                Vector2 touchPosition = Input.GetTouch(0).position;
                endpos = new Vector3(touchPosition.x, 0, touchPosition.y);
                final = endpos - startpos;
                length = final.magnitude;
                spinningBottle.RotateAround(Vector3.zero, Vector3.up, length);
            }
        }
        */
    }


    void swipeFingerOnScreen()
    {
        if (Input.GetMouseButton(0))
        {
            startpos = Input.mousePosition;
            swipeTime += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            shouldSpinn = true;
            endpos = Input.mousePosition;
            final = endpos - startpos;
            if(startpos.y < endpos.y)
            {
                swipeDirection = Vector3.back;
            }else
            {
                swipeDirection = Vector3.forward;
            }
            length = final.magnitude;
            spinnBootleTime = swipeTime * length;
            if (spinnBootleTime > 15)
            {
                spinnBootleTime = 15;
            }
            Debug.Log(spinnBootleTime);
            swipeTime = 0f;
        }
    }
}
