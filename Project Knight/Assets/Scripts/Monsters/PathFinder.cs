using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Rigidbody2D monster;
    public BasicMonster monsterScript;
    public Rigidbody2D player;
    public WorldSettings worldSettings;
    public int move;

    public GameObject[] structures;
    public GameObject[] monsters;
    public GameObject[] banPosition;

    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        monsterScript = GetComponent<BasicMonster>();
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        move = worldSettings.move;
    }

    void Update()
    {
        structures = GameObject.FindGameObjectsWithTag("Structure");
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        banPosition = structures.Concat(monsters).ToArray();
        if (move != worldSettings.move)
        {
            Vector2 endPosition = player.transform.position;
            Vector2 startPosition = transform.position;
            Vector2 targetPosition = new Vector2(0, 0);

            bool upPosiyion = false;
            bool downPosiyion = false;
            bool leftPosiyion = false;
            bool rightPosiyion = false;

            if (endPosition.x < startPosition.x)
            {
                targetPosition = new Vector2(startPosition.x - 1, startPosition.y);
                monsterScript.monsterPosition = monster.transform.position;
            }
            else if (endPosition.y < startPosition.y)
            {
                targetPosition = new Vector2(startPosition.x, startPosition.y - 1);
                monsterScript.monsterPosition = monster.transform.position;
            }
            else if (endPosition.x > startPosition.x)
            {
                targetPosition = new Vector2(startPosition.x + 1, startPosition.y);
                monsterScript.monsterPosition = monster.transform.position;
            }
            else if (endPosition.y > startPosition.y)
            {
                targetPosition = new Vector2(startPosition.x, startPosition.y + 1);
                monsterScript.monsterPosition = monster.transform.position;
            }
            for (int i = 0; i < banPosition.Length; i++)
            {
                if (targetPosition == new Vector2(banPosition[i].transform.position.x, banPosition[i].transform.position.y))
                {
                    targetPosition = startPosition;
                }
            }
            if (Random.RandomRange(0, 10) == 5) targetPosition = startPosition;
            monster.transform.position = targetPosition;
            move++;
        }
    }
}
