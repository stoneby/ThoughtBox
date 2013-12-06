using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColliderCombiner))]
public class ColliderCombinerEditor : Editor
{
    private ColliderCombiner combiner;

    void OnEnable()
    {
        if(combiner == null)
        {
            combiner = target as ColliderCombiner;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Combine"))
        {
            Combine();
        }
    }

    private void Combine()
    {
        combiner.CombineBounds();
    }
}
