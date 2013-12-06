using System.Collections.Generic;
using UnityEngine;

public class FrameTestMenu : MonoBehaviour
{
    public List<AbstractMove> MoverList;

    public GameObject Example;

    private string frameCount = "0";
    private string animationTime = "0";
    private float frameRate;
    private Animation exampleAnimation;
    private AnimationState exampleAnimationState;

	void OnGUI()
	{
	    if(GUILayout.Button("Start"))
	    {
	        MoverList.ForEach(mover => mover.Move());
	    }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Frame: ");
	    frameCount = GUILayout.TextField(frameCount, 100, GUILayout.Width(40));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        if(GUILayout.Button("Sample"))
        {
            var count = int.Parse(frameCount);
            exampleAnimationState.time = 1 / frameRate * count;
            exampleAnimationState.speed = 0;
            exampleAnimation.Play();
        }
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Time: ");
	    animationTime = GUILayout.TextField(animationTime, 40);
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Sample"))
        {
            var time = float.Parse(animationTime);
            exampleAnimationState.time = time;
            exampleAnimationState.speed = 0;
            exampleAnimation.Play();
        }
	}

    void Awake()
    {
        exampleAnimation = Example.animation;
        exampleAnimationState = exampleAnimation["Example"];
        frameRate = exampleAnimation.GetClip("Example").frameRate;
    }
}
