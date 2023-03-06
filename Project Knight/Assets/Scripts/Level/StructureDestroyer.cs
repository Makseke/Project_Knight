using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDestroyer : MonoBehaviour
{
    private Rigidbody2D player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > transform.position.y + 20)
        {
            Destroy(gameObject);
        }
    }
}
