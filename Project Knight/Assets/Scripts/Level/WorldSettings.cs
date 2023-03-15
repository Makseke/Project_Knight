using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    public int difficult;
    public string biom;
    public int move;

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
        
    }
}
