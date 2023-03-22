using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvilMushroomPathFinder : MonoBehaviour
{
    public Rigidbody2D monster;
    public BoxCollider2D worldCheck;
    public BasicMonster monsterScript;
    public HeroConroler player;
    public WorldSettings worldSettings;
    public int move;

    public GameObject[] structures;
    public GameObject[] monsters;
    public GameObject[] banPosition;
    private List<Vector2> movingPositions = new List<Vector2>();


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

    public Vector2 lastPosition;

    public bool upPosition = false;
    public bool downPosition = false;
    public bool rightPosition = false;
    public bool leftPosition = false;

    private void Move()
    {
        //проверка движения персонажа
        if (endPosition != monster.position && isReverse == false)
        {
            isMoving = true;
        }
        //если персонаж находится в движении то просчитывается вектор движения исходя из кривой анимации
        if (isMoving == true)
        {
            speed = movemetCurve.Evaluate(time);
            monster.position = Vector2.MoveTowards(monster.position, endPosition, speed * Time.fixedDeltaTime);
        }
        //проверка закончил ли персонаж движение
        if (monster.position == endPosition && isReverse == false)
        {
            monster.position = endPosition;
            startPosition = monster.position;
            isMoving = false;
            isReverse = false;
            time = Time.deltaTime;
        }
        //проверка вернулся ли персонаж на точку старта в случае столкновения с объектом
        else if (monster.position == startPosition && isReverse == true)
        {
            monster.position = endPosition;
            startPosition = monster.position;
            isMoving = false;
            isReverse = false;
            endPosition = startPosition;
            time = Time.deltaTime;
        }
    }

    void PathFind()
    {
        structures = GameObject.FindGameObjectsWithTag("Structure");
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        banPosition = structures.Concat(monsters).ToArray();

        Vector2 endPosition_ = player.endPosition;
        Vector2 startPosition_ = transform.position;
        Vector2 targetPosition = new Vector2(0, 0);

        movingPositions.Clear();
        movingPositions.Add(new Vector2(transform.position.x, transform.position.y + 1));
        movingPositions.Add(new Vector2(transform.position.x, transform.position.y - 1));
        movingPositions.Add(new Vector2(transform.position.x + 1, transform.position.y));
        movingPositions.Add(new Vector2(transform.position.x - 1, transform.position.y));

        upPosition = false;
        downPosition = false;
        rightPosition = false;
        leftPosition = false;


        lastPosition = startPosition;
        if (endPosition_.x < startPosition_.x)
        {
            leftPosition = true;
        }
        if (endPosition_.x > startPosition_.x)
        {
            rightPosition = true;
        }
        if (endPosition_.y < startPosition_.y)
        {
            downPosition = true;
        }
        if (endPosition_.y > startPosition_.y)
        {
            upPosition = true;
        }

        for (int i = 0; i < banPosition.Length; i++)
        {
            if ((new Vector2(transform.position.x, transform.position.y + 1) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && upPosition == true) ||
                (new Vector2(transform.position.x, transform.position.y - 1) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && downPosition == true) ||
                (new Vector2(transform.position.x + 1, transform.position.y) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && rightPosition == true) ||
                (new Vector2(transform.position.x - 1, transform.position.y) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && leftPosition == true))
            {
                Vector2 banPosition_ = new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y);
                movingPositions.Remove(banPosition_);
            }
        }

        if (upPosition == false)
        {
            Vector2 banPosition_ = new Vector2(transform.position.x, transform.position.y + 1);
            movingPositions.Remove(banPosition_);
        }
        if (downPosition == false)
        {
            Vector2 banPosition_ = new Vector2(transform.position.x, transform.position.y - 1);
            movingPositions.Remove(banPosition_);
        }
        if (rightPosition == false)
        {
            Vector2 banPosition_ = new Vector2(transform.position.x + 1, transform.position.y);
            movingPositions.Remove(banPosition_);
        }
        if (leftPosition == false)
        {
            Vector2 banPosition_ = new Vector2(transform.position.x - 1, transform.position.y);
            movingPositions.Remove(banPosition_);
        }

        if (isMoving == false) startPosition = monster.position;
        if (movingPositions.Count != 0)
        {
            endPosition = movingPositions[Random.Range(0, movingPositions.Count)];
        }
        else
        {
            endPosition = startPosition;
        }
        move++;
    }

    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        monsterScript = GetComponent<BasicMonster>();
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroConroler>();
        move = worldSettings.move;

        startPosition = transform.position;
        endPosition = transform.position;
    }

    void LateUpdate()
    {
        Move();
        if (move != worldSettings.move && isMoving == false)
        {
            PathFind();
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endPosition = startPosition;

            isReverse = true;
        }
        if (collision.gameObject.tag == "Monster")
        {
            PathFinder monster = collision.gameObject.GetComponent<PathFinder>();
            if (Random.Range(0, 1000) > Random.Range(0, 1000))
            {
                endPosition = startPosition;
                isReverse = true;

            }
            else
            {
                monster.endPosition = monster.startPosition;
                monster.isReverse = true;

            }
        }
    }
}
