using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingStripUT : MonoBehaviour
{
    private RollingStrip rollupStrip;

    void OnGUI()
    {
        if (GUILayout.Button("Test"))
        {
            StartCoroutine("DoTest");
        }
    }

    void Awake()
    {
        rollupStrip = GetComponent<RollingStrip>();

        if (rollupStrip == null)
        {
            Debug.LogError("Could not find RollingStrip of current game object.");
        }
    }

    void Start()
    {
        StartCoroutine("DoTest");
    }

    IEnumerator DoTest()
    {
        rollupStrip.Direction = SpinDirection.Down;
        rollupStrip.CursorIndex = 0;

        yield return null;

        Debug.Log("Init.");
        Debug.Log(rollupStrip);

        yield return null;
        
        rollupStrip.Next();

        Debug.Log("Next");
        Debug.Log(rollupStrip);

        yield return null;
        
        rollupStrip.Direction = SpinDirection.Up;
        rollupStrip.Next();

        Debug.Log("Back");
        Debug.Log(rollupStrip);

        yield return null;
        
        rollupStrip.Next();
        rollupStrip.Next();

        Debug.Log("Back 2 step");
        Debug.Log(rollupStrip);
    }
}
