using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private RoomsType variants;
    private Rigidbody2D player;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsType>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //Destroy(gameObject, waitTime);
        //Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        if (!spawned)
        {
            rand = Random.RandomRange(0, variants.basicRooms.Length);
            Instantiate(variants.basicRooms[rand], transform.position, variants.basicRooms[rand].transform.rotation);
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
