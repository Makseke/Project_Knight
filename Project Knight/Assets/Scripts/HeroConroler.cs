using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    public Rigidbody2D hero;
    public Vector2 touchStartPosition;
    public Vector2 touchEndPosition;
    public Vector2 touchfinalPosition;

    private float speed;
    public AnimationCurve movemetCurve;

    public bool isMoving = false;

    public Vector2 startPosition;
    public Vector2 endPosition;
    public float time;

    private void Move()
    {
        if (endPosition != hero.position)
        {
            isMoving = true;
        }
        if (isMoving == true)
        {
            speed = movemetCurve.Evaluate(time);
            hero.transform.position = Vector2.MoveTowards(hero.position, endPosition, speed * Time.deltaTime);
            time += Time.deltaTime;
        }
        else
        {

        }
        if (hero.position == endPosition)
        {
            isMoving = false;
            time = 0.0f;
        }
    }

    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        time = Time.deltaTime;
    }

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

                if (x_dif > 100 || y_dif > 100)
                {
                    if (x_dif > y_dif)
                    {
                        if (touchStartPosition.x > touchEndPosition.x)
                        {
                            if (isMoving == false)
                            {
                                endPosition = hero.position - new Vector2(1, 0);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                endPosition = hero.position + new Vector2(1, 0);
                            }
                        }
                    }
                    else
                    {
                        if (touchStartPosition.y > touchEndPosition.y)
                        {
                            if (isMoving == false)
                            {
                                endPosition = hero.position - new Vector2(0, 1);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                endPosition = hero.position + new Vector2(0, 1);
                            }
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}


