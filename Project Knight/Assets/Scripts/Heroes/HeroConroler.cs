using UnityEngine;

    public class HeroConroler : MonoBehaviour
{
    //������ �� ������ � ���������� �����
    public Rigidbody2D player;
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
        //�������� �������� ���������
        if (endPosition != player.position && isReverse == false)
        {
            isMoving = true;
            lastMove_2 = lastMove_1;
            lastMove_1 = isMoving;
        }
        //���� �������� ��������� � �������� �� �������������� ������ �������� ������ �� ������ ��������
        if (isMoving == true && Mathf.Abs(last_dif_1) >= Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            player.position = Vector2.MoveTowards(player.position, endPosition, speed * Time.fixedDeltaTime);

            last_dif_2 = last_dif_1;
            last_dif_1 = Mathf.Abs((startPosition.x - player.position.x) + (startPosition.y - player.position.y));

            time += Time.deltaTime;
        }
        //���� �������� �� �������� �������� � ������� 1 �������, ������������� ������� �� ��������� ������
        else if (isMoving == true && Mathf.Abs(last_dif_1) < Mathf.Abs(last_dif_2))
        {
            speed = movemetCurve.Evaluate(time);
            player.transform.position = Vector2.MoveTowards(player.position, startPosition, speed * Time.fixedDeltaTime);
            time += Time.deltaTime;
            isReverse = true;
        }
        //�������� �������� �� �������� ��������
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
        //�������� �������� �� �������� �� ����� ������ � ������ ������������ � ��������
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
        if (isMoving == false)
        {
            startPosition = new Vector2((int)startPosition.x, (int)startPosition.y);
            endPosition = new Vector2((int)endPosition.x, (int)endPosition.y);
        }
    }

    //�������� ������ � ��������� ������� ��� ������������
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
        //����� ������ ��� ��������
        Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
        WorldMove();
        //�������� ���������� �����
        if (Input.touchCount == 1) {
            //������ �������
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
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
                float x_dif = 0;
                float y_dif = 0;
                //������������� ������� ����� x � y ������������ ������ � ����� ����
                x_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.x) - Mathf.Abs(touchEndPosition.x));
                y_dif = Mathf.Abs(Mathf.Abs(touchStartPosition.y) - Mathf.Abs(touchEndPosition.y));
                //�������� ������ ��������
                if (x_dif > 100 || y_dif > 100)
                {
                    if (isMoving == false)
                    {
                        startPosition = player.position;
                        endPosition = player.position;

                        if (x_dif > y_dif)
                        {
                            endPosition.x = touchStartPosition.x > touchEndPosition.x ? player.position.x - 1 : player.position.x + 1;
                        }
                        else
                        {
                            endPosition.y = touchStartPosition.y > touchEndPosition.y ? player.position.y - 1 : player.position.y + 1;
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
        // ��������� ��� �������, �� ������� ����� ���������
        if (collision.gameObject.tag == "Structure")
        {
            // ���� ��� ���������, �� ����� �� ����� ��������� � ������������ ������� �� ��������� �������
            endPosition = startPosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
        }
        else if (collision.gameObject.tag == "Monster")
        {
            // ���� ��� ������, �� ���������, ��������� �� ����� �� ������������� �����������
            if (player.position.y == (int)player.position.y && player.position.x == (int)player.position.x)
            {
                // ���� �� ������������� �����������, �� ������� �������
                HeroStats playerScript = GetComponent<HeroStats>();
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                playerScript.health -= monster.atackPoints;
            }
            else
            {
                // ���� �� ������� �����������, �� �������� ����� ���� � ������� � ��������� ���� �� ������� �����������
                PathFinder monsterScript = collision.gameObject.GetComponent<PathFinder>();
                HeroStats playerScript = GetComponent<HeroStats>();
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                if (startPosition != endPosition)
                {
                    if (monsterScript.startPosition != endPosition)
                    {
                        // ���� ���� ��������, �� ��������� � �������
                    }
                    else
                    {
                        // ���� ���� ������������, �� ����� ������������ ������� �� ��������� ������� � ������� �������
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
                    // ���� ����� �� ��������, �� ��� ������������� � �������� ������ ������� ���� ������
                    playerScript.health -= monster.atackPoints;
                }
            }
        }
    }

}


