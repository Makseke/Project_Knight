using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRenderer : MonoBehaviour
{
    private WorldSettings worldSettings;
    private SpriteRenderer renderer;
    public Sprite[] monsterSptite;

    void Start()
    {
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        renderer = GetComponent<SpriteRenderer>();

        if (worldSettings.difficult == 0)
        {
            renderer.sprite = monsterSptite[0];
        }
        else
        {
            renderer.sprite = monsterSptite[1];
        }
    }
}
