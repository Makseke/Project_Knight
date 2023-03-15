using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BasicMonster : MonoBehaviour
{
    public Collider2D trigerCollider;
    public Rigidbody2D monster;
    public GameObject player;

    public Vector2 monsterPosition;
    public Vector2 playerPosition;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI maxHp;

    public int healPoints;
    public int atackPoints;

    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        trigerCollider = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        monsterPosition = monster.transform.position;
        //��������� �������� �� ��������� ��� ����� �������� � �����
        healPoints = 100;
        atackPoints = 10;
        //�������� ������������ ���������� ��� ����������� ����� �������� �����
        maxHp.text = healPoints.ToString();
        hp.gameObject.SetActive(false);
        maxHp.gameObject.SetActive(false);
    }

    void Update()
    {
        //�������������� �������� ���� ����� ���� ������� ������
        if (player.transform.position.y > monster.transform.position.y + 33)
        {
            Destroy(gameObject);
        }
        //����������� �������� �������� ������
        hp.text = healPoints.ToString();
        if ((Mathf.Abs(player.transform.position.y - monster.transform.position.y)) <= 1 && (Mathf.Abs(player.transform.position.x - monster.transform.position.x) <= 1))
        {
            hp.gameObject.SetActive(true);
            maxHp.gameObject.SetActive(true);
        }
        else
        {
            hp.gameObject.SetActive(false);
            maxHp.gameObject.SetActive(false);
        }
    }
}
