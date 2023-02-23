
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class ShowFPS : MonoBehaviour
{

    public static float fps;
    public static bool showFPS;
    public GameObject hero;
    //public static float firstTouch;
    //public static float secondTouch;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
    }

    void OnGUI()
    {
        fps = 1.0f / Time.deltaTime;
        GUI.skin.label.fontSize = 40;
        GUILayout.Label("FPS: " + (int)fps);
        //GUILayout.Label("FT: " + (int)firstTouch);
        //GUILayout.Label("ST: " + (int)secondTouch);
    }
}
