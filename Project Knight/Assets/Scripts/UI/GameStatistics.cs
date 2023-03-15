using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;

public class GameStatistics : MonoBehaviour
{

    
    public int score;
    public int bestPlayerPosition;
    private Rigidbody2D player;

    //����� Score �� �����
    void OnGUI()
    {
        
        GUI.Label(new Rect(460, 100, 150, 100), "Score: " + score);

    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        score = 0;
        bestPlayerPosition = (int)player.position.y;
    }
    // Score ������� �� ������� ������
    private void Update()
    {
        if (bestPlayerPosition < (int)player.position.y)
        {
            bestPlayerPosition = (int)player.position.y;
            score++;
        }

    }

}
