using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiAutoSize : MonoBehaviour
{
    public bool upSide;
    public bool downSide;
    public bool buttonDownSide;
    public int buttonID = 1;

    public Camera camera;
    public GameObject sprite;
    public GameObject spriteButton;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (downSide == true)
        {
            gameObject.transform.position = new Vector3(0, camera.transform.position.y - camera.orthographicSize + sprite.GetComponent<SpriteRenderer>().bounds.size.y/2, -3);
        }
        else if (buttonDownSide == true)
        {
            gameObject.transform.position = new Vector3(sprite.GetComponent<SpriteRenderer>().bounds.size.x / 5 * buttonID - 5.5f, camera.transform.position.y - camera.orthographicSize + sprite.GetComponent<SpriteRenderer>().bounds.size.y / 2 + spriteButton.GetComponent<SpriteRenderer>().bounds.size.y + 0.5f, -3);
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
