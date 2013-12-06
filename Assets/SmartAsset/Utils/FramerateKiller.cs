using UnityEngine;

public class FramerateKiller : MonoBehaviour
{
    public int BusyFrameRate = 60;
    public int FrameToKill = 10;

    private int counter;

    // Use this for initialization
    void Awake()
    {
        Debug.Log("Target frame rate: " + Application.targetFrameRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(counter % BusyFrameRate == 0)
        {
            Kill();
        }
        ++counter;
    }

    void Kill()
    {
        var startTime = Time.realtimeSinceStartup;
        var killTime = Time.deltaTime * FrameToKill;
        var i = 0;
        const int step = 10;
        while(true)
        {
            if(i >= step)
            {
                var endTime = Time.realtimeSinceStartup;
                var time = endTime - startTime;
                if(time > killTime)
                {
                    Debug.Log("Kill time: " + time);
                    break;
                }
                i = 0;
            }
            i++;
        }
    }
}
