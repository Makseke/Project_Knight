using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private LevelVariants variants;
    private Rigidbody2D player;
    private int rand;
    private bool spawned = false;

    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelVariants>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    public void Spawn()
    {
        if (!spawned)
        {
            rand = Random.RandomRange(0, variants.ForestTiles.Length);
            Instantiate(variants.ForestTiles[rand], transform.position, variants.ForestTiles[rand].transform.rotation);
            spawned = true;
        }
    }

    private void DoubleSpawn(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player.position.y > transform.position.y - 33)
        {
            Spawn();
            Destroy(gameObject);
        }
    }
}
