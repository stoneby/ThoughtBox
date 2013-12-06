using System.Collections.Generic;
using UnityEngine;

public class GestureMenu : MonoBehaviour
{
    public GameObject RollerContainer;

    private List<GameObject> children;

    void OnGUI()
    {
        if(GUILayout.Button("Generate Box Collider"))
        {
            GenerateCollider();
        }

        if(GUILayout.Button("Display Information"))
        {
            PrintColliderInformation();
        }
    }

    private void GenerateCollider()
    {
        // world combined bounds.
        var combinedBounds = children[0].collider.bounds;
        combinedBounds.Encapsulate(children[children.Count - 1].collider.bounds);

        // relativate bounds to local box collider.
        var boxCollider = RollerContainer.GetComponent<BoxCollider>();
        boxCollider.center = combinedBounds.center - RollerContainer.transform.position;
        boxCollider.size = new Vector3(combinedBounds.size.x / RollerContainer.transform.localScale.x,
                                       combinedBounds.size.y / RollerContainer.transform.localScale.y,
                                       combinedBounds.size.z / RollerContainer.transform.localScale.z);

        //children[0].collider.bounds = new Bounds(Vector3.zero, Vector3.one);
    }

    private void PrintColliderInformation()
    {
        RollerContainer.AddComponent<BoxCollider>();
        //PrintColliderInformation(RollerContainer);
        children.ForEach(PrintColliderInformation);
    }

    private static void PrintColliderInformation(GameObject child)
    {
        var boxCollider = child.GetComponent<BoxCollider>();
        Debug.Log("child: " + child.name + ", position: " + child.transform.position +
                  ", collider: " + child.transform.collider.bounds +
                  ", \nbox collider center: " + boxCollider.center +
                  ", extend: " + boxCollider.extents +
                  ", size: " + boxCollider.size +
                  ", bounds: " + boxCollider.bounds +
                  ", \nrender's bounds: " + child.renderer.bounds);
    }

    void Start()
    {
        if(children == null)
        {
            children = new List<GameObject>();
            for(var i = 0; i < RollerContainer.transform.childCount; ++i)
            {
                children.Add(RollerContainer.transform.GetChild(i).gameObject);
            }
        }
    }
}