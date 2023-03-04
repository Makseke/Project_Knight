using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    //������ �� ������ � ���������� �����
    public Rigidbody2D hero;
    public Vector2 touchStartPosition;
    public Vector2 touchEndPosition;
    public Vector2 touchfinalPosition;

    //���������� ��� ����������� �������� ��������
    private float speed;
    public AnimationCurve movemetCurve;

    //�������� �������� � �������� � ������ ������������
    public bool isMoving = false;
    public bool isReverse = false;

    //������� ����� � ����� �������� ���������
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float time;

    public float last_dif_1;
    public float last_dif_2;

    private void Move()
    {
        //�������� �������� ���������
        if (endPosition != hero.position && isReverse == false)
        {
            isMoving = true;
        }
        //���� �������� ��������� � �������� �� �������������� ������ �������� ������ �� ������ ��������
        if (isMoving == true && Mathf.Abs(last_dif_1) >= Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            hero.position = Vector2.MoveTowards(hero.position, endPosition, speed * Time.fixedDeltaTime);

            last_dif_2 = last_dif_1;
            last_dif_1 = Mathf.Abs((startPosition.x - hero.position.x) + (startPosition.y - hero.position.y));

            time += Time.deltaTime;
        }
        //���� �������� �� �������� �������� � ������� 1 �������, ������������� ������� �� ��������� ������
        else if (isMoving == true && Mathf.Abs(last_dif_1) < Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            hero.transform.position = Vector2.MoveTowards(hero.position, startPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
            isReverse = true;
        }
        //�������� �������� �� �������� ��������
        if (hero.position == endPosition && isReverse == false)
        {
            isMoving = false;
            isReverse = false;
            time = 0.0f;
            last_dif_1 = 0.0f;
            last_dif_2 = 0.0f;
        }
        //�������� �������� �� �������� �� ����� ������ � ������ ������������ � ��������
        else if (hero.position == startPosition && isReverse == true)
        {
            isMoving = false;
            isReverse = false;
            time = 0.0f;
            endPosition = startPosition;
            last_dif_1 = 0.0f;
            last_dif_2 = 0.0f;
        }
    }

    //�������� ������ � ��������� ������� ��� ������������
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        time = Time.fixedDeltaTime;
        endPosition = hero.position;
    }

    void Update()
    {
        //����� ������ ��� ��������
        Move();
        //�������� ���������� �����
        if (Input.touchCount == 1) {
            //������ �������
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //Debug.Log("start");
                touchStartPosition = Input.GetTouch(0).position;
                touchEndPosition = Input.GetTouch(0).position;
            }
            //������� �������
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchEndPosition = Input.GetTouch(0).position;
            }
            //����� ������� ��� ������ �����������
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //Debug.Log("end");
                float x_dif = 0;
                float y_dif = 0;
                //������������� ������� ����� x � y ������������ ������ � ����� ����
                x_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.x) - Mathf.Abs(touchEndPosition.x));
                y_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.y) - Mathf.Abs(touchEndPosition.y));
                //�������� ������ ��������
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
            }
        }
    }

    void FixedUpdate()
    {
        //Move();
    }
}


