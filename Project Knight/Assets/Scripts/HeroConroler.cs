using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    public Rigidbody2D hero;
    public Vector2 touchStartPosition;
    public Vector2 touchEndPosition;
    public Vector2 touchfinalPosition;

    public float speed = 0.2f;

    public bool isMoving = false;
    public bool inProcess = false;
    public int moveDirection = 0;

    public Vector2 startPosition;
    public Vector2 endPosition;

    private void Move()
    {
        if (endPosition != hero.position)
        {
            isMoving = true;
            inProcess = true;
        }
        if (isMoving == true)
        {
                hero.transform.position = Vector2.MoveTowards(hero.position, endPosition, speed * Time.deltaTime);
        }
        else
        {

        }
        if (hero.position == endPosition)
        {
            isMoving = false;
            inProcess = false;
        }
    }

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
                touchStartPosition = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchEndPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("end");
                float x_dif = 0;
                float y_dif = 0;
                if (Mathf.Abs(touchStartPosition.x) > Mathf.Abs(touchEndPosition.x))
                {
                    x_dif = Mathf.Abs(touchStartPosition.x) - Mathf.Abs(touchEndPosition.x);
                }
                else
                {
                    x_dif = Mathf.Abs(touchEndPosition.x) - Mathf.Abs(touchStartPosition.x);
                }

                if (Mathf.Abs(touchStartPosition.y) > Mathf.Abs(touchEndPosition.y))
                {
                    y_dif = Mathf.Abs(touchStartPosition.y) - Mathf.Abs(touchEndPosition.y);
                }
                else
                {
                    y_dif = Mathf.Abs(touchEndPosition.y) - Mathf.Abs(touchStartPosition.y);
                }

                if (x_dif > y_dif)
                {
                    if (touchStartPosition.x > touchEndPosition.x)
                    {
                        //hero.MovePosition(hero.position - new Vector2(1.0f, 0.0f));
                        //hero.transform.position = Vector2.Lerp(hero.position, new Vector2(hero.position.x - 1, 0), speed * Time.deltaTime);
                        //hero.transform.position = Vector2.MoveTowards(hero.position, new Vector2(hero.position.x - 1, 0), Time.deltaTime * speed);

                        if (inProcess == false)
                        {
                            endPosition = hero.position - new Vector2(1, 0);
                        }
                    }
                    else
                    {
                        //hero.MovePosition(hero.position + new Vector2(1.0f, 0.0f));
                        if (inProcess == false)
                        {
                            endPosition = hero.position + new Vector2(1, 0);
                        }
                    }
                }
                else
                {
                    if (touchStartPosition.y > touchEndPosition.y)
                    {
                        //hero.MovePosition(hero.position - new Vector2(0.0f, 1.0f));
                        if (inProcess == false)
                        {
                            endPosition = hero.position - new Vector2(0, 1);
                        }
                    }
                    else
                    {
                        //hero.MovePosition(hero.position + new Vector2(0.0f, 1.0f));
                        if (inProcess == false)
                        {
                            endPosition = hero.position + new Vector2(0, 1);
                        }
                    }
                }

            }
        }
    }

    void LateUpdate()
    {
        Move();
    }
}


