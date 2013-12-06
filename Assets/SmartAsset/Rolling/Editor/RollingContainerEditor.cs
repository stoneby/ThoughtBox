using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(RollingContainer))]
public class RollingContainerEditor : Editor
{
    private RollingContainer rollingContainer;

    void OnEnable()
    {
        rollingContainer = target as RollingContainer;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
