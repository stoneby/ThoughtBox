using System.Collections.Generic;
using UnityEngine;

public class ColliderCombiner : MonoBehaviour
{
    /// <summary>
    /// List that holding children game object to combine
    /// </summary>
    public List<GameObject> CombineList;
    
    /// <summary>
    /// Offset between current combiner to children to combine
    /// </summary>
    public Vector3 OffSet;

    void Awake()
    {
        CombineList = new List<GameObject>();
    }

    private bool Validate()
    {
        if(CombineList == null)
        {
            Debug.LogWarning("CombineList could not be null.");
            return false;
        }

        CombineList.ForEach(item =>
                                {
                                    if(item.collider == null)
                                    {
                                        Debug.LogWarning("Game object: " + item.name +
                                                         " does not have collider to be attached to!");
                                    }
                                });
        return true;
    }

    /// <summary>
    /// Combine bounds of combine list to current game object
    /// </summary>
    /// <remarks>Box collider is used for current game object</remarks>
    public void CombineBounds()
    {
        if (!Validate())
        {
            return;
        }

        var combinedBounds = CombineList[0].collider.bounds;
        CombineList.ForEach(item => combinedBounds.Encapsulate(item.collider.bounds));

        var boxCollider = gameObject.GetComponent<BoxCollider>() ?? gameObject.AddComponent<BoxCollider>();
        boxCollider.center = combinedBounds.center - transform.position;
        boxCollider.center += OffSet;
        boxCollider.size = new Vector3(combinedBounds.size.x / transform.localScale.x,
                                       combinedBounds.size.y / transform.localScale.y,
                                       combinedBounds.size.z / transform.localScale.z);
    }
}