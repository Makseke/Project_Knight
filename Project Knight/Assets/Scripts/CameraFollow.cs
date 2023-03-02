using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.U2D.Animation;

public class CameraFollow : MonoBehaviour
{
    
    private Transform player;
    private Transform cameraPos;


    // Поиск камеры и персонажа по тегам и задаем начальную позицию камеры
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.position = new Vector3(0, player.position.y + 5.0f, -10);
    }

    //Следование камеры за персонажем по оси Y
    void LateUpdate()
    {
        if (player.position.y > cameraPos.position.y)
        {
            cameraPos.position = new Vector3(0, player.position.y, -10);
        } 
    }
}






