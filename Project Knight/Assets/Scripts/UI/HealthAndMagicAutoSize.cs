using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class HealthAndMagicAutoSize : MonoBehaviour
{
    private HeroStats player;

    private Vector3 indicatorPosition;
    private Vector3 indicatorSize;
    private float startSize;

    public bool health;
    public bool magic;

    public int lastInd;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroStats>();
        startSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        indicatorPosition = transform.position;
        indicatorSize = transform.localScale;
        if (health == true)
        {
            lastInd = player.health;
        }
        if (magic == true)
        {
            lastInd = player.magic;
        }
    }

    void Update()
    {
        if (health == true && player.health != lastInd)
        {
            if (player.health <= 0)
            {
                transform.localScale = new Vector3(0, 1, 1);
            }
            else if (player.health > player.healthMAX)
            {
                transform.localScale = new Vector3(5, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(indicatorSize.x * ((float)player.health / (float)player.healthMAX), 1, 1);
                transform.position = new Vector3(indicatorPosition.x + (indicatorPosition.x - (indicatorPosition.x * ((float)player.health / (float)player.healthMAX))), transform.position.y, transform.position.z);
            }
            lastInd = player.health;
        }
        if (magic == true && player.magic != lastInd)
        {
            if (player.magic <= 0)
            {
                transform.localScale = new Vector3(0, 1, 1);
            }
            else if (player.magic > player.magicMAX)
            {
                transform.localScale = new Vector3(5, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(indicatorSize.x * ((float)player.magic / (float)player.magicMAX), 1, 1);
                transform.position = new Vector3(indicatorPosition.x - (indicatorPosition.x - (indicatorPosition.x * ((float)player.magic / (float)player.magicMAX))), transform.position.y, transform.position.z);
            }
            lastInd = player.magic;
        }
    }
}
