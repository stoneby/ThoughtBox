using System.Collections.Generic;
using UnityEngine;

public class ColorUpdater : RollingItemUpdater
{
    public List<Color> ColorList;

    public override void Change(GameObject stripItem, RollingStrip rollingStrip, int index)
    {
        stripItem.renderer.material.color = ColorList[rollingStrip.StripContainer.StripList[rollingStrip.Buffer[index]].Id];
        var text = stripItem.transform.GetChild(0).GetComponent<TextMesh>();
        text.text = "" + rollingStrip.Buffer[index];
    }
}
