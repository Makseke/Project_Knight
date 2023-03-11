using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Rigidbody2D tryer;

    public Vector2[] canGo;
    public Vector2[] cantGo;

    public Vector2[] path;

    public Dictionary<Vector2, Vector2> canGo_;

    public Vector2 startPosition;
    public Vector2 endPosition;

    public Vector2 nowPosition;
    public Vector2 tryPosition;

    public void FindPath()
    {
        startPosition = gameObject.transform.position;
        nowPosition = startPosition;
        canGo[0] = startPosition;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                tryer.transform.position = new Vector2(nowPosition.x - 1, nowPosition.y);
                tryPosition = new Vector2(nowPosition.x - 1, nowPosition.y);
                if (tryer.position != tryPosition)
                {
                    cantGo[0] = new Vector2(nowPosition.x - 1, nowPosition.y);
                }
            }
        }
    }

    void Start()
    {
        tryer = GetComponent<Rigidbody2D>();
        FindPath();
    }

    void Update()
    {
        
    }
}
