using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    //?????? ?? ?????? ? ?????????? ?????
    public Rigidbody2D hero;
    public Vector2 touchStartPosition;
    public Vector2 touchEndPosition;
    public Vector2 touchfinalPosition;

    //?????????? ??? ??????????? ???????? ????????
    private float speed;
    public AnimationCurve movemetCurve;

    //???????? ???????? ? ???????? ? ?????? ????????????
    public bool isMoving = false;
    public bool isReverse = false;

    //??????? ????? ? ????? ???????? ?????????
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float time;

    public float last_dif_1;
    public float last_dif_2;

    private WorldSettings worldSettings;
    private bool lastMove_1;
    private bool lastMove_2;

    private void WorldMove()
    {
        if (lastMove_1 == false && lastMove_2 == true)
        {
            worldSettings.move += 1;
        }
    }

    private void Move()
    {
        //???????? ???????? ?????????
        if (endPosition != hero.position && isReverse == false)
        {
            isMoving = true;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
        //???? ???????? ????????? ? ???????? ?? ?????????????? ?????? ???????? ?????? ?? ?????? ????????
        if (isMoving == true && Mathf.Abs(last_dif_1) >= Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            hero.position = Vector2.MoveTowards(hero.position, endPosition, speed * Time.fixedDeltaTime);

            last_dif_2 = last_dif_1;
            last_dif_1 = Mathf.Abs((startPosition.x - hero.position.x) + (startPosition.y - hero.position.y));

            time += Time.deltaTime;
        }
        //???? ???????? ?? ???????? ???????? ? ??????? 1 ???????, ????????????? ??????? ?? ????????? ??????
        else if (isMoving == true && Mathf.Abs(last_dif_1) < Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            hero.transform.position = Vector2.MoveTowards(hero.position, startPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
            isReverse = true;
        }
        //???????? ???????? ?? ???????? ????????
        if (hero.position == endPosition && isReverse == false)
        {
            hero.position = endPosition;

            isMoving = false;
            isReverse = false;
            time = 0.0f;
            last_dif_1 = 0.0f;
            last_dif_2 = 0.0f;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
        //???????? ???????? ?? ???????? ?? ????? ?????? ? ?????? ???????????? ? ????????
        else if (hero.position == startPosition && isReverse == true)
        {
            hero.position = endPosition;

            isMoving = false;
            isReverse = false;
            time = 0.0f;
            endPosition = startPosition;
            last_dif_1 = 0.0f;
            last_dif_2 = 0.0f;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
    }

    //???????? ?????? ? ????????? ??????? ??? ????????????
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        time = Time.fixedDeltaTime;
        endPosition = hero.position;
        lastMove_2 = false;
        lastMove_1 = false;
    }

    void Update()
    {
        //????? ?????? ??? ????????
        Move();
        WorldMove();
        //???????? ?????????? ?????
        if (Input.touchCount == 1) {
            //?????? ???????
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Debug.Log("start");
                touchStartPosition = Input.GetTouch(0).position;
                touchEndPosition = Input.GetTouch(0).position;
            }
            //??????? ???????
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchEndPosition = Input.GetTouch(0).position;
            }
            //????? ??????? ??? ?????? ???????????
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Debug.Log("end");
                float x_dif = 0;
                float y_dif = 0;
                //????????????? ??????? ????? x ? y ???????????? ?????? ? ????? ????
                x_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.x) - Mathf.Abs(touchEndPosition.x));
                y_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.y) - Mathf.Abs(touchEndPosition.y));
                //???????? ?????? ????????
                if (x_dif > 100 || y_dif > 100)
                {
                    if (x_dif > y_dif)
                    {
                        if (touchStartPosition.x > touchEndPosition.x)
                        {
                            if (isMoving == false)
                            {
                                startPosition = hero.position;
                                endPosition = hero.position - new Vector2(1, 0);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                startPosition = hero.position;
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
                                startPosition = hero.position;
                                endPosition = hero.position - new Vector2(0, 1);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                startPosition = hero.position;
                                endPosition = hero.position + new Vector2(0, 1);
                            }
                        }
                    }
                }
                else
                {
                    lastMove_1 = false;
                    lastMove_2 = true;
                    WorldMove();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Structure")
        {
            endPosition = startPosition;
        }
    }
}


