using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    public GameObject[] variants;
    public GameObject structure;
    public Rigidbody2D player;
    private int random;


    void Start()
    {
        random = Random.RandomRange(0, variants.Length);
        structure = Instantiate(variants[random], transform.position, variants[random].transform.rotation);
        structure.AddComponent<StructureDestroyer>();
        Destroy(gameObject);
    }
}
