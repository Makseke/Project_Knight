using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.U2D.Animation;

public class CameraFollow : MonoBehaviour
{
    
    private Transform player;
    private Transform cameraPos;


    // ����� ������ � ��������� �� ����� � ������ ��������� ������� ������
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.position = new Vector3(0, player.position.y + 2.0f, 0);
    }

    //���������� ������ �� ���������� �� ��� Y
    void LateUpdate()
    {
        Vector2 temp = transform.position;

        
        
        if (player.position.y > cameraPos.position.y)
        {
            temp.y = player.position.y;
            transform.position = temp;
        } 
            
        
    }
}






