using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Rigidbody2D monster;
    public BoxCollider2D worldCheck;
    public BasicMonster monsterScript;
    public Rigidbody2D player;
    public WorldSettings worldSettings;
    public int move;

    public GameObject[] structures;
    public GameObject[] monsters;
    public GameObject[] banPosition;
    //public Vector2[] movingPositions;
    private List<Vector2> movingPositions = new List<Vector2>();


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

    private Vector2 lastPosition;

    public bool upPosition = false;
    public bool downPosition = false;
    public bool rightPosition = false;
    public bool leftPosition = false;

    private void Move()
    {
        //???????? ???????? ?????????
        if (endPosition != monster.position && isReverse == false)
        {
            isMoving = true;
        }
        //???? ???????? ????????? ? ???????? ?? ?????????????? ?????? ???????? ?????? ?? ?????? ????????
        if (isMoving == true)
        {
            speed = movemetCurve.Evaluate(time);
            monster.position = Vector2.MoveTowards(monster.position, endPosition, speed * Time.fixedDeltaTime);
        }
        //???????? ???????? ?? ???????? ????????
        if (monster.position == endPosition && isReverse == false)
        {
            monster.position = endPosition;
            startPosition = monster.position;
            isMoving = false;
            isReverse = false;
            time = Time.deltaTime;
        }
        //???????? ???????? ?? ???????? ?? ????? ?????? ? ?????? ???????????? ? ????????
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

    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        monsterScript = GetComponent<BasicMonster>();
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        move = worldSettings.move;

        startPosition = transform.position;
        endPosition = transform.position;
    }

    void Update()
    {
        Move();

        structures = GameObject.FindGameObjectsWithTag("Structure");
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        banPosition = structures.Concat(monsters).ToArray();
        if (move != worldSettings.move && isMoving == false)
        {
            Vector2 endPosition_ = player.transform.position;
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
                //targetPosition = new Vector2(startPosition_.x - 1, startPosition_.y);
                //monsterScript.monsterPosition = lastPosition;
                leftPosition = true;
            }
            if (endPosition_.x > startPosition_.x)
            {
                //targetPosition = new Vector2(startPosition_.x + 1, startPosition_.y);
                //monsterScript.monsterPosition = lastPosition;
                rightPosition = true;
            }
            if (endPosition_.y < startPosition_.y)
            {
                //targetPosition = new Vector2(startPosition_.x, startPosition_.y - 1);
                //monsterScript.monsterPosition = lastPosition;
                downPosition = true;
            }
            if (endPosition_.y > startPosition_.y)
            {
                //targetPosition = new Vector2(startPosition_.x, startPosition_.y + 1);
                //monsterScript.monsterPosition = lastPosition;
                upPosition = true;
            }

            //for (int i = 0; i < 4; i++)
            //{
            //    Vector2 tryPosition = new Vector2(0,0);
            //    if (i == 0 && upPosition == true) { tryPosition = new Vector2(transform.position.x, transform.position.y + 1);}
            //    if (i == 1 && downPosition == true) { tryPosition = new Vector2(transform.position.x, transform.position.y - 1);}
            //    if (i == 2 && leftPosition == true) { tryPosition = new Vector2(transform.position.x - 1, transform.position.y);}
            //    if (i == 3 && rightPosition == true) { tryPosition = new Vector2(transform.position.x + 1, transform.position.y);}
            //}

            for (int i = 0; i < banPosition.Length; i++)
            {
                if ((new Vector2(transform.position.x, transform.position.y + 1) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && upPosition == true) ||
                    (new Vector2(transform.position.x, transform.position.y - 1) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && downPosition == true) ||
                    (new Vector2(transform.position.x + 1, transform.position.y) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && rightPosition == true) ||
                    (new Vector2(transform.position.x - 1, transform.position.y - 1) == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y) && leftPosition == true))
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

            //if (Random.RandomRange(0, 10) == 5) targetPosition = startPosition_;
            if (isMoving == false) startPosition = monster.position;
            if (movingPositions.Count != 0)
            {
                endPosition = movingPositions[Random.RandomRange(0, movingPositions.Count)];
            }
            else
            {
                endPosition = startPosition;
                Debug.Log("NO WAY");
            }
            move++;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            monster.transform.position = lastPosition;
            endPosition = startPosition;
            player.GetComponent<HeroConroler>().endPosition = player.GetComponent<HeroConroler>().startPosition;

            isReverse = true;
        }
    }

    public void OnTriggereExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            monster.transform.position = lastPosition;
            endPosition = startPosition;
            player.GetComponent<HeroConroler>().endPosition = player.GetComponent<HeroConroler>().startPosition;

            isReverse = true;
        }
    }
}

