using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    //ссылка на Игрока и координаты тачей
    public Rigidbody2D player;
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

    public float last_dif_1;
    public float last_dif_2;

    private WorldSettings worldSettings;
    private bool lastMove_1;
    private bool lastMove_2;

    private void WorldMove()
    {
        if (lastMove_1 == true && lastMove_2 == false)
        {
            worldSettings.move += 1;
        }
    }

    private void Move()
    {
        //проверка движения персонажа
        if (endPosition != player.position && isReverse == false)
        {
            isMoving = true;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
        //если персонаж находится в движении то просчитывается вектор движения исходя из кривой анимации
        if (isMoving == true && Mathf.Abs(last_dif_1) >= Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            player.position = Vector2.MoveTowards(player.position, endPosition, speed * Time.fixedDeltaTime);

            last_dif_2 = last_dif_1;
            last_dif_1 = Mathf.Abs((startPosition.x - player.position.x) + (startPosition.y - player.position.y));

            time += Time.deltaTime;
        }
        //если персонаж не закончил движение в течении 1 секунды, перестраивает маршрут на последнюю клетку
        else if (isMoving == true && Mathf.Abs(last_dif_1) < Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            player.transform.position = Vector2.MoveTowards(player.position, startPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
            isReverse = true;
        }
        //проверка закончил ли персонаж движение
        if (player.position == endPosition && isReverse == false)
        {
            player.position = endPosition;

            isMoving = false;
            isReverse = false;
            time = 0.0f;
            last_dif_1 = 0.0f;
            last_dif_2 = 0.0f;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
        //проверка вернулся ли персонаж на точку старта в случае столкновения с объектом
        else if (player.position == startPosition && isReverse == true)
        {
            player.position = endPosition;

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

    //получает доступ к основному объекту для передвижения
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        time = Time.fixedDeltaTime;
        endPosition = player.position;
        lastMove_2 = false;
        lastMove_1 = false;
    }

    void Update()
    {
        //вызов метода для движения
        Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
        WorldMove();
        //проверка количества тачей
        if (Input.touchCount == 1) {
            //начало нажатия
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
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
                                startPosition = player.position;
                                endPosition = player.position - new Vector2(1, 0);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                startPosition = player.position;
                                endPosition = player.position + new Vector2(1, 0);
                            }
                        }
                    }
                    else
                    {
                        if (touchStartPosition.y > touchEndPosition.y)
                        {
                            if (isMoving == false)
                            {
                                startPosition = player.position;
                                endPosition = player.position - new Vector2(0, 1);
                            }
                        }
                        else
                        {
                            if (isMoving == false)
                            {
                                startPosition = player.position;
                                endPosition = player.position + new Vector2(0, 1);
                            }
                        }
                    }
                }
                else
                {
                    lastMove_1 = true;
                    lastMove_2 = false;
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
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
        }
        if (collision.gameObject.tag == "Monster")
        {
            if (player.position.y == (int)player.position.y && player.position.x == (int)player.position.x)
            {
                HeroStats playerScript = GetComponent<HeroStats>();
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                playerScript.health -= monster.atackPoints;
            }
            else
            {
                PathFinder monsterScript = collision.gameObject.GetComponent<PathFinder>();
                HeroStats playerScript = GetComponent<HeroStats>();
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                if (startPosition != endPosition)
                {
                    if (monsterScript.startPosition != endPosition)
                    {

                    }
                    else
                    {
                        endPosition = startPosition;
                        transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
                        monster.healPoints -= playerScript.atack;
                        if (monster.healPoints <= 0)
                        {
                            Destroy(collision.gameObject);
                        }
                        else
                        {
                            playerScript.health -= monster.atackPoints;
                        }
                    }
                }
                else
                {
                    playerScript.health -= monster.atackPoints;
                }
            }
        }
    }
}


