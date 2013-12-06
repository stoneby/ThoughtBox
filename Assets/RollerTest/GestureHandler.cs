using System;
using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    public bool Enabled;

    private bool dragTrigger;
    private bool scrollTrigger;
    private Vector3 lastMousePosition;

    public event EventHandler<EventArgs> FireScroll;

    void Start()
    {
    }

    void OnMouseExit()
    {
        // no dragging mode.
        if(!dragTrigger)
        {
            return;
        }

        FireScrollEvent();

        Debug.Log("Exit: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseDrag()
    {
        if(scrollTrigger)
        {
            return;
        }

        UpdatePosition();

        dragTrigger = true;

        Debug.Log("Drag: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseDown()
    {
        scrollTrigger = false;
        dragTrigger = false;

        Debug.Log("Down: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseUp()
    {
        // no dragging mode.
        if(!dragTrigger)
        {
            return;
        }

        dragTrigger = false;

        FireScrollEvent();

        Debug.Log("Up: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void UpdatePosition()
    {
        // update position except first time drag is triggered.
        if(dragTrigger)
        {
            var currentMousePosition = Input.mousePosition;
            var delta = currentMousePosition - lastMousePosition;
            transform.position += delta;
        }

        lastMousePosition = Input.mousePosition;
    }

    void FireScrollEvent()
    {
        scrollTrigger = true;

        if(FireScroll != null)
        {
            FireScroll(this, new EventArgs());
        }
    }
}
