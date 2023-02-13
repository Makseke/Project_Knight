using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroConroler : MonoBehaviour
{

    public Rigidbody2D hero;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public Vector2 finalPosition;

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
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
                float x_dif = 0;
                float y_dif = 0;
                if (Mathf.Abs(startPosition.x) > Mathf.Abs(endPosition.x))
                {
                    x_dif = Mathf.Abs(startPosition.x) - Mathf.Abs(endPosition.x);
                }
                else
                {
                    x_dif = Mathf.Abs(endPosition.x) - Mathf.Abs(startPosition.x);
                }

                if (Mathf.Abs(startPosition.y) > Mathf.Abs(endPosition.y))
                {
                    y_dif = Mathf.Abs(startPosition.y) - Mathf.Abs(endPosition.y);
                }
                else
                {
                    y_dif = Mathf.Abs(endPosition.y) - Mathf.Abs(startPosition.y);
                }

                if (x_dif > y_dif)
                {
                    if (startPosition.x > endPosition.x)
                    {
                        hero.MovePosition(hero.position - new Vector2(1.0f, 0.0f));
                    }
                    else
                    {
                        hero.MovePosition(hero.position + new Vector2(1.0f, 0.0f));
                    }
                }
                else
                {
                    if (startPosition.y > endPosition.y)
                    {
                        hero.MovePosition(hero.position - new Vector2(0.0f, 1.0f));
                    }
                    else
                    {
                        hero.MovePosition(hero.position + new Vector2(0.0f, 1.0f));
                    }
                }

            }
        }
       
    }
}
