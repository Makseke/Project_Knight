using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;

public class GameStatistics : MonoBehaviour
{

    
    public int ScoreOld;
    public int ScoreNew;
    private Transform Player;

    //Вывод Score на экран
    void OnGUI()
    {
        
        GUI.Label(new Rect(460, 100, 150, 100), "Score: " + ScoreNew);

    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        ScoreOld = 0;
    }
    // Score зависит от позиции игрока
    private void LateUpdate()
    {
        ScoreOld = (int)Player.position.y;

        if (ScoreOld > ScoreNew)
        {
            ScoreNew = ScoreOld;
        }

    }

}
