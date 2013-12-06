using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerMenu : MonoBehaviour
{
    public List<RollingContainer> RollerList;

    public List<float> DelayList;

    public string Count;
    public string Speed;

	void OnGUI()
	{
	    if (GUILayout.Button("Spin"))
	    {
	        StartCoroutine("Spin");
	    }

        if(GUILayout.Button("Stop"))
        {
            Stop();
        }

        GUILayout.Label("Speed: ");
	    Speed = GUILayout.TextField(Speed);

        GUILayout.Label("Count: ");
	    Count = GUILayout.TextField(Count);
	}

    IEnumerator Spin()
    {
        for(var i = 0; i < RollerList.Count; ++i)
        {
            var delay = DelayList[i];
            yield return new WaitForSeconds(delay);

            var roller = RollerList[i];
            roller.Generate();
            roller.Speed = float.Parse(Speed);
            var count = int.Parse(Count);
            roller.Spin(count);
        }
    }

    void Stop()
    {
        RollerList.ForEach(roller => roller.Stop());
    }
}
