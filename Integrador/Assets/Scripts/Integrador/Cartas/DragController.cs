using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool dragActive = false;
    private Vector2 screenPos;
    private Vector2 worldPosition;
    private Drag finalDrag;

    private void Awake()
    {
        DragController[] controllers = Object.FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (dragActive)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Drop();
                return;
            }

        }
        if (Input.touchCount > 0)
        {
            screenPos = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPos);

        if (dragActive) 
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Drag draggable = hit.transform.gameObject.GetComponent<Drag>();
                if (draggable != null)
                {
                    finalDrag = draggable;
                    DragInicial();
                }
            }
        }
    }

    void DragInicial()
    {
        dragActive = true;
    }

    void Drag()
    {
        finalDrag.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }

    void Drop()
    {
        dragActive = false;
    }
}
