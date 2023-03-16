using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private Rigidbody2D player;
    private HeroConroler playerScript;
    private Collider2D playerCollider;
    public int health;
    public int atack;
    public int level;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<HeroConroler>();
        playerCollider = GetComponent<Collider2D>();
        health = 100;
        atack = 20;
    }
}
