using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private Rigidbody2D player;
    private Collider2D playerCollider;
    public int health;
    public int atack;
    public int level;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        health = 100;
        atack = 20;
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (player.position.y == (int)player.position.y && player.position.x == (int)player.position.x)
            {
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                health -= monster.atackPoints;
                Debug.Log("M to H");
            }
            else
            {
                BasicMonster monster = collision.gameObject.GetComponent<BasicMonster>();
                monster.healPoints -= atack;
                if (monster.healPoints <= 0)
                {
                    Destroy(collision.gameObject);
                    Debug.Log("Destroy");
                }
                Debug.Log("H to M");
            }
        }
    }
}
