using UnityEngine;
using System.Collections;

public class CoroutineTest : MonoBehaviour
{
    void NoneFrame()
    {
        var beginTime = Time.realtimeSinceStartup;
        Debug.Log("None frame coroutine." + " at frame: " + Time.frameCount);
        Debug.Log("None frame coroutine finish." + " at frame: " + Time.frameCount + ", time elapse: " + (Time.realtimeSinceStartup - beginTime));
    }

    IEnumerator OneFrame()
    {
        var beginTime = Time.realtimeSinceStartup;
        Debug.Log("One frame coroutine begins." + " at frame: " + Time.frameCount);
        yield return null;
        Debug.Log("One frame coroutine finish." + " at frame: " + Time.frameCount + ", time elapse: " + (Time.realtimeSinceStartup - beginTime));
        Debug.Log("One frame coroutine Last frame time: " + Time.deltaTime + " at frame: " + Time.frameCount);
    }

    IEnumerator ManyFrame()
    {
        var beginTime = Time.realtimeSinceStartup;
        Debug.Log("Many frame coroutine begins." + " at frame: " + Time.frameCount);
        yield return new WaitForSeconds(1f);
        var elapseTime = Time.realtimeSinceStartup - beginTime;
        Debug.Log("Many frame coroutine finish" + " at frame: " + Time.frameCount + ", time elapse: " + elapseTime);
        Debug.Log("Many frame coroutine Last frame time: " + Time.deltaTime + " at frame: " + Time.frameCount);
        Debug.Log("Many frame coroutine time elapse without last frame time: " + (elapseTime - Time.deltaTime));
    }

    IEnumerator OneFrameCaller()
    {
        var beginTime = Time.realtimeSinceStartup;
        Debug.Log("One frame caller begins." + " at frame: " + Time.frameCount);
        yield return StartCoroutine("ManyFrame");
        Debug.Log("One frame caller gogogogogoogogogogo.");
        Debug.Log("One frame caller finish. at frame: " + Time.frameCount + ", time elapse: " + (Time.realtimeSinceStartup - beginTime));
    }

    void OnGUI()
    {
        if(GUILayout.Button("No frame start"))
        {
            StartCoroutine("NoneFrame");
        }

        if(GUILayout.Button("One frame start"))
        {
            StartCoroutine("OneFrame");
        }

        if(GUILayout.Button("Many frame start"))
        {
            StartCoroutine("ManyFrame");
        }

        if(GUILayout.Button("One frame caller start"))
        {
            StartCoroutine("OneFrameCaller");
        }
    }
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
