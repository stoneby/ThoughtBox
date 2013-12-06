using UnityEngine;

public class MoveAnalyzer : MonoBehaviour
{
    public bool IsPlaying { get; set; }

    public float StartTime { get; set; }
    public float EndTime { get; set; }

    private float counter;

    public void Play()
    {
        counter = 0f;
        IsPlaying = true;
        StartTime = Time.time;

        Debug.Log("Animation start time: " + StartTime);
    }

    public void Stop()
    {
        IsPlaying = false;
        EndTime = Time.time;

        Debug.Log("Animation stop time: " + EndTime);
        Debug.Log("Animation duration: " + (EndTime - StartTime));

        Debug.Log("Frame time duration: " + counter);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying)
        {
            Debug.Log("Delta time: " + Time.deltaTime + ", current frame rate: " + 1 / Time.deltaTime);
            counter += Time.deltaTime;
        }
    }

}
