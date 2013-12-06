using UnityEngine;

public class TextureUpdater : RollingItemUpdater
{
    private Object[] objects;

    public override void Change(GameObject stripItem, RollingStrip rollingStrip, int index)
    {
        stripItem.renderer.material.SetTexture("_MainTex", objects[rollingStrip.StripContainer.StripList[rollingStrip.Buffer[index]].Id] as Texture);
    }

    void Awake()
    {
        objects = Resources.LoadAll("Textures", typeof(Texture));
    }
}
