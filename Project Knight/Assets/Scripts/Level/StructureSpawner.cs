using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    public GameObject[] variants;
    public GameObject structure;
    private int random;


    void Start()
    {
        random = Random.RandomRange(0, variants.Length);
        structure = Instantiate(variants[random], new Vector3(transform.position.x, transform.position.y, -0.01f)  , variants[random].transform.rotation);
        structure.AddComponent<StructureDestroyer>();
        Destroy(gameObject);
    }
}
