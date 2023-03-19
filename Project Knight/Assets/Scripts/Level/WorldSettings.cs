using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public int difficult;
    public string biom;
    public int move;

    public GameObject[] banPosition;


    void Start()
    {
        move = 0;
        difficult = 0;
        if (difficult == 0)
        {
            biom = "forest";
        }
    }

    void Update()
    {
        banPosition = FindObjectsOfType<GameObject>().Where(obj => obj.CompareTag("Structure") || obj.CompareTag("Monster")).ToArray();
    }
}


