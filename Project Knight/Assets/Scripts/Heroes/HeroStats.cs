using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    private Rigidbody2D player;
    private HeroConroler playerScript;
    private Collider2D playerCollider;

    public int health;
    public int healthMAX;

    public int magic;
    public int magicMAX;

    public int atack;
    public int level;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI hpMAX;

    public TextMeshProUGUI mana;
    public TextMeshProUGUI manaMAX;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<HeroConroler>();
        playerCollider = GetComponent<Collider2D>();

        healthMAX = 100;
        health = healthMAX;

        magicMAX = 100;
        magic = magicMAX;

        atack = 20;
    }

    private void Update()
    {
        hp.text = health.ToString();
        hpMAX.text = healthMAX.ToString();
        mana.text = magic.ToString();
        manaMAX.text = magicMAX.ToString();
    }
}
