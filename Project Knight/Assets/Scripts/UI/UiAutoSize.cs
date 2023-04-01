using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiAutoSize : MonoBehaviour
{
    public bool upSide;
    public bool downSide;

    public Camera camera;
    public GameObject sprite;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (downSide == true)
        {
            gameObject.transform.position = new Vector3(0, camera.transform.position.y - camera.orthographicSize + sprite.GetComponent<SpriteRenderer>().bounds.size.y/2, -3);
        }
        else if(upSide == true)
        {
            gameObject.transform.position = new Vector3(0, camera.transform.position.y + camera.orthographicSize - sprite.GetComponent<SpriteRenderer>().bounds.size.y / 2, -3);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {

    }
}
