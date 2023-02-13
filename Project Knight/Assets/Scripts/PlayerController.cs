using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    public Transform player;

    public void OnBeginDrag(PointerEventData eventData) 
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if(eventData.delta.x > 0)
            {
            player.position += Vector3.right;
            }
            else
            {
            player.position += Vector3.left;
            }
        }
        else 
        {
            if (eventData.delta.y > 0)
            {
                player.position += Vector3.up;
            }
            else
            {
                player.position += Vector3.down;
            }
        }
    }

    public void OnDrag(PointerEventData eventData) 
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
