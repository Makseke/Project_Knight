using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Rigidbody2D monster;
    private BoxCollider2D worldCheck;
    private BasicMonster monsterScript;
    private HeroConroler player;
    private WorldSettings worldSettings;
    public int move;

    //public GameObject[] structures;
    public GameObject[] monsters;
    public List<Vector2> stayPosition = new List<Vector2>();
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

    private float time;

    public Vector2 lastPosition;

    private bool upPosition = false;
    private bool downPosition = false;
    private bool rightPosition = false;
    private bool leftPosition = false;

    private int toBanPosition = 0;
    private int toUnBanPosition = 0;

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
        if (isMoving == false)
        {
            startPosition = new Vector2((int)startPosition.x, (int)startPosition.y);
            endPosition = new Vector2((int)endPosition.x, (int)endPosition.y);
        }
    }

    void PathFind()
    {
        worldSettings.banPosition = FindObjectsOfType<GameObject>().Where(obj => obj.CompareTag("Structure") || obj.CompareTag("Monster")).ToArray();
        //GameObject[] structures = Physics2D.OverlapCircleAll(GameObject.FindGameObjectWithTag("Player").transform.position, 5f).Where(obj => obj.CompareTag("Structure")).Select(obj => obj.gameObject).ToArray();

        Vector2 endPosition_ = player.endPosition;
        Vector2 startPosition_ = transform.position;

        if (stayPosition.Count != 0)
        {
            if (toUnBanPosition == 7)
            {
                stayPosition.Remove(stayPosition.First());
                toUnBanPosition = 0;
                Debug.Log("UNBAN");
            }
            toUnBanPosition++;
        }

        movingPositions.Clear();
        movingPositions.Add(new Vector2(transform.position.x, transform.position.y + 1));
        movingPositions.Add(new Vector2(transform.position.x, transform.position.y - 1));
        if (transform.position.x + 1 != 6) movingPositions.Add(new Vector2(transform.position.x + 1, transform.position.y));
        if (transform.position.x - 1 != -6) movingPositions.Add(new Vector2(transform.position.x - 1, transform.position.y));

        upPosition = false;
        downPosition = false;
        rightPosition = false;
        leftPosition = false;

        for (int i = 0; i < stayPosition.Count; i++)
        {
            if (startPosition == stayPosition[i])
            {
                upPosition = true;
                downPosition = true;
                rightPosition = true;
                leftPosition = true;
            }
        }


            lastPosition = startPosition;
        leftPosition = endPosition_.x < startPosition_.x ? true : leftPosition;
        rightPosition = endPosition_.x > startPosition_.x ? true : rightPosition;
        downPosition = endPosition_.y < startPosition_.y ? true : downPosition;
        upPosition = endPosition_.y > startPosition_.y ? true : upPosition;

        for (int i = 0; i < worldSettings.banPosition.Length; i++)
        {
            if ((new Vector2(transform.position.x, transform.position.y + 1) == new Vector2(worldSettings.banPosition[i].transform.position.x, worldSettings.banPosition[i].transform.position.y)) ||
                (new Vector2(transform.position.x, transform.position.y - 1) == new Vector2(worldSettings.banPosition[i].transform.position.x, worldSettings.banPosition[i].transform.position.y)) ||
                (new Vector2(transform.position.x + 1, transform.position.y) == new Vector2(worldSettings.banPosition[i].transform.position.x, worldSettings.banPosition[i].transform.position.y)) ||
                (new Vector2(transform.position.x - 1, transform.position.y) == new Vector2(worldSettings.banPosition[i].transform.position.x, worldSettings.banPosition[i].transform.position.y)))
            {
                Vector2 banPosition_ = new Vector2(worldSettings.banPosition[i].transform.position.x, worldSettings.banPosition[i].transform.position.y);
                movingPositions.Remove(banPosition_);
            }
        }

        for (int i = 0; i < stayPosition.Count; i++)
        {
            if ((new Vector2(transform.position.x, transform.position.y + 1) == new Vector2(stayPosition[i].x, stayPosition[i].y)) ||
                (new Vector2(transform.position.x, transform.position.y - 1) == new Vector2(stayPosition[i].x, stayPosition[i].y)) ||
                (new Vector2(transform.position.x + 1, transform.position.y) == new Vector2(stayPosition[i].x, stayPosition[i].y)) ||
                (new Vector2(transform.position.x - 1, transform.position.y) == new Vector2(stayPosition[i].x, stayPosition[i].y)))
            {
                Vector2 banPosition_ = new Vector2(stayPosition[i].x, stayPosition[i].y);
                movingPositions.Remove(banPosition_);
            }
        }


        //если игрок находится в "зоне видимости" прокладывает маршрут в его строну, иначе ходит в случайных доступных направлениях
        if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 5)
        {
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
        }

        if (isMoving == false) startPosition = monster.position;
        if (movingPositions.Count != 0)
        {
            endPosition = movingPositions[Random.Range(0, movingPositions.Count)];
        }
        else
        {
            endPosition = startPosition;
            if (Mathf.Abs(player.startPosition.x - transform.position.x) > 1.5f || Mathf.Abs(player.startPosition.y - transform.position.y) > 1.5f)
            {
                stayPosition.Add(startPosition);
                Debug.Log("banAdd");
            }
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
            if (GetComponent<Transform>().gameObject.GetInstanceID() > collision.gameObject.GetComponent<Transform>().gameObject.GetInstanceID())
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

