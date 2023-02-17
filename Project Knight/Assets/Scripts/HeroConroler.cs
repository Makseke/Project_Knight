using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    //ссылка на Игрока и координаты тачей
    public Rigidbody2D hero;
    public Vector2 touchStartPosition;
    public Vector2 touchEndPosition;
    public Vector2 touchfinalPosition;

    //переменные для определения скорости движения
    private float speed;
    public AnimationCurve movemetCurve;

    //проверка движения и возврата в случае столкновения
    public bool isMoving = false;
    public bool isReverse = false;

    //позиции начал и конца движения персонажа
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float time;

    //отвечает за передвижение персонажа
    private void Move()
    {
        //проверка движения персонажа
        if (endPosition != hero.position && isReverse == false)
        {
            isMoving = true;
        }
        //если персонаж находится в движении то просчитывается вектор движения исходя из кривой анимации
        if (isMoving == true && time < 1)
        {
            speed = movemetCurve.Evaluate(time);
            hero.position = Vector2.MoveTowards(hero.position, endPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
        }
        //если персонаж не закончил движение в течении 1 секунды, перестраивает маршрут на последнюю клетку
        else if (isMoving == true && time > 1)
        {
            speed = movemetCurve.Evaluate(time);
            hero.transform.position = Vector2.MoveTowards(hero.position, startPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
            isReverse= true;
        }
        //проверка закончил ли персонаж движение
        if (hero.position == endPosition && isReverse == false)
        {
            isMoving = false;
            isReverse = false;
            time = 0.0f;
        }
        //проверка вернулся ли персонаж на точку старта в случае столкновения с объектом
        else if (hero.position == startPosition && isReverse == true)
        {
            isMoving = false;
            isReverse = false;
            time = 0.0f;
            endPosition = startPosition;
        }
    }

    //получает доступ к основному объекту для передвижения
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        time = Time.fixedDeltaTime;
        endPosition = hero.position;
    }

    void Update()
    {
        //вызов метода для движения
        Move();
        //проверка количества тачей
        if (Input.touchCount == 1) {
            //начало нажатия
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("start");
                touchStartPosition = Input.GetTouch(0).position;
                touchEndPosition = Input.GetTouch(0).position;
            }
            //процесс нажатия
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchEndPosition = Input.GetTouch(0).position;
            }
            //конец нажатия или ошибка определения
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("end");
                float x_dif = 0;
                float y_dif = 0;
                //высчитывается разница между x и y координатами начала и конца тача
                x_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.x) - Mathf.Abs(touchEndPosition.x));
                y_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.y) - Mathf.Abs(touchEndPosition.y));
                //задается вектор движения
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


