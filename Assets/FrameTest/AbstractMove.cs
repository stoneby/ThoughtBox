using System;
using UnityEngine;

public abstract class AbstractMove : MonoBehaviour
{
    public event EventHandler<EventArgs> OnPlay;
    public event EventHandler<EventArgs> OnStop;

    public MoveAnalyzer Analyzer;

    public abstract void Move();

	void Play()
	{
	    Analyzer.Play();

        if(OnPlay != null)
        {
            OnPlay(this, new EventArgs());
        }
	}

    void Stop()
    {
        Analyzer.Stop();

        if(OnStop != null)
        {
            OnStop(this, new EventArgs());
        }
    }
}
