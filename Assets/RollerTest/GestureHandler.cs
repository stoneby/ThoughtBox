using System;
using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    public event EventHandler<SpeedEventArgs> FireScroll;
    public event EventHandler<EventArgs> StopScroll;
    public event EventHandler<UpdateEventArgs> UpdateScroll;

    public SpeedAnalyzer SpeedAnalyzer;

    void Start()
    {
        SpeedAnalyzer.RollStart += OnRollStart;
    }

    private void OnRollStart(object sender, SpeedEventArgs args)
    {
        FireScrollEvent(args);
    }

    void OnMouseExit()
    {
        var currentWorldPosition =
            Camera.mainCamera.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y,
                                                             (transform.position.z -
                                                              Camera.mainCamera.transform.position.z)));
        //SpeedAnalyzer.OnEnd(currentWorldPosition);
        Debug.Log("Exit: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseDrag()
    {
        var currentWorldPosition =
            Camera.mainCamera.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y,
                                                             (transform.position.z -
                                                              Camera.mainCamera.transform.position.z)));
        var delta = SpeedAnalyzer.OnUpdate(currentWorldPosition);
        
        // update children's position, keep parent untransform.
        if(UpdateScroll != null)
        {
            UpdateScroll(this, new UpdateEventArgs { Position = delta });
        }

        Debug.Log("Drag: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseDown()
    {
        StopScrollEvent();

        Debug.Log("Down: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void OnMouseUp()
    {
        var currentWorldPosition =
            Camera.mainCamera.ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y,
                                                             (transform.position.z -
                                                              Camera.mainCamera.transform.position.z)));
        SpeedAnalyzer.OnEnd(currentWorldPosition);

        Debug.Log("Up: " + Input.mousePosition + ", frame count: " + Time.frameCount);
    }

    void FireScrollEvent(SpeedEventArgs args)
    {
        if(FireScroll != null)
        {
            FireScroll(this, args);
        }
    }

    void StopScrollEvent()
    {
        if(StopScroll != null)
        {
            StopScroll(this, new EventArgs());
        }
    }

    void Update()
    {
        var ray = Camera.mainCamera.ScreenPointToRay(new Vector3(0, 0, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 p = Camera.mainCamera.ScreenToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.mainCamera.transform.position.z));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.5f);
    }
}
