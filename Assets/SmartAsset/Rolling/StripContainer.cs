using System;
using System.Collections.Generic;
using UnityEngine;

public class StripContainer : MonoBehaviour
{
    public TextAsset Text;

    public List<StripItem> StripList
    {
        get
        {
            if (stripList == null)
            {
                Init();
            }
            return stripList;
        }
    }

    private List<StripItem> stripList = null;

    private void Init()
    {
        stripList = new List<StripItem>();

        var lines = Text.text.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var tokens = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var item = new StripItem {Id = int.Parse(tokens[0]), Name = tokens[1]};
            StripList.Add(item);
        }
    }
}
