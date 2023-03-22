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
        //установка значений по умолчанию для очков здоровья и атаки
        healPoints = 100;
        atackPoints = 10;
        //создания графического интерфейса для отображения очков здоровья врага
        maxHp.text = healPoints.ToString();
        hp.gameObject.SetActive(false);
        maxHp.gameObject.SetActive(false);
    }

    void Update()
    {
        //автоматическое удаление если игрок ушел слишком далеко
        if (player.transform.position.y > monster.transform.position.y + 15)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>().monsterCount--;
        }
        //отображение значений здоровья врагов
        hp.text = healPoints.ToString();
        if ((Mathf.Abs(player.transform.position.y - monster.transform.position.y)) <= 1.5f && (Mathf.Abs(player.transform.position.x - monster.transform.position.x) <= 1.5f))
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
