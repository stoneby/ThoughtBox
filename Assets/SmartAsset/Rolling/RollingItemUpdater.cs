using UnityEngine;

public abstract class RollingItemUpdater : MonoBehaviour
{
    public abstract void Change(GameObject stripItem, RollingStrip rollingStrip, int index);
}
