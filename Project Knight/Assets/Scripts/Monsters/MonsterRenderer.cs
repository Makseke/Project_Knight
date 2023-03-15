using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRenderer : MonoBehaviour
{
    private WorldSettings worldSettings;
    private SpriteRenderer renderer;

    void Start()
    {
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
}
