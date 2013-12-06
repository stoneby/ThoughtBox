using System;
using UnityEngine;

public class AnimationMove : AbstractMove
{
    private Animation moveAnimation;
    private AnimationState moveState;

    public float Speed;

    private int index;

    public override void Move()
    {
        index = 0;
        moveState.speed = Speed;
        moveAnimation.Play();
    }

    // Use this for initialization
    void Awake()
    {
        moveAnimation = gameObject.animation;
        moveState = moveAnimation["Move"];
        Debug.Log("Move animation: speed-" + moveState.speed + ", time-" + moveState.time);

        OnPlay += OnAnimationPlay;
        OnStop += OnAnimationStop;
    }

    private void OnAnimationPlay(object sender, EventArgs args)
    {
        
    }

    private void OnAnimationStop(object sender, EventArgs args)
    {
        //animation
    }

    // Update is called once per frame
    void Update()
    {
        if(Analyzer.IsPlaying)
        {
            Debug.Log("Move animation: time-" + moveState.time);
        }
    }
}