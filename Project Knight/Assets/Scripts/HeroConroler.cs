using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroConroler : MonoBehaviour
{

    public Rigidbody2D hero;
    public bool isStarted;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 finalPosition;
    public Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1) {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("start");
                startPosition = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                endPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("end");
                if(startPosition.x > endPosition.x)
                {
                    hero.MovePosition(hero.position - new Vector2(1.0f, 0.0f));
                }
                else
                {
                    hero.MovePosition(hero.position + new Vector2(1.0f, 0.0f));
                }
            }
        }
       
    }
}
