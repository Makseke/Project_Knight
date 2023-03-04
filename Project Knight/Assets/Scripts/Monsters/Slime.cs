using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Collider2D trigerCollider;
    public Rigidbody2D monster;
    public GameObject player;

    public Vector2 monsterPosition;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI maxHp;

    public int healPoints;

    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        trigerCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        monsterPosition = monster.transform.position;
        healPoints = 100;
        maxHp.text = healPoints.ToString();
        hp.gameObject.SetActive(false);
        maxHp.gameObject.SetActive(false);
    }


    void Update()
    {
        if (player.transform.position.y > monster.transform.position.y + 33)
        {
            Destroy(gameObject);
        }
        hp.text = healPoints.ToString();
        if (Mathf.Abs(Mathf.Abs(player.transform.position.y) - Mathf.Abs(monster.transform.position.y)) <= 1 && Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(monster.transform.position.x)) <= 1)
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hited by Player");
            player.GetComponent<HeroConroler>().endPosition = player.GetComponent<HeroConroler>().startPosition;
            healPoints -= 20;
        }
        if (healPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            monster.transform.position = monsterPosition;
        }
    }
}
