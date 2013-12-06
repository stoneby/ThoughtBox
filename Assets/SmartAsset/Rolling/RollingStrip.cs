using System.Collections.Generic;
using UnityEngine;

public enum SpinDirection
{
    Up,
    Down
}

public class RollingStrip : MonoBehaviour
{
    public StripContainer StripContainer;

    public SpinDirection Direction;

    public int VisibleCount;

    public int BufferCount;

    public int CursorIndex { get; set; }

    public List<int> Buffer { get; private set; }

    private int totalCount;

    public void Next(int n)
    {
        CursorIndex = (Direction == SpinDirection.Down) ? CursorIndex - n : CursorIndex + n;
        CursorIndex = Round(CursorIndex);

        Sync();
    }

    public void Next()
    {
        Next(1);
    }

    private int Round(int index)
    {
        index = (index) < 0 ? (index + totalCount) : (index >= totalCount) ? (index - totalCount) : index;
        return index;
    }

    private void Sync()
    {
        Buffer.Clear();

        for (var i = 0; i < BufferCount; ++i)
        {
            var index = CursorIndex - BufferCount + i;
            index = Round(index);
            Buffer.Add(index);
        }

        for (var i = 0; i < VisibleCount; ++i)
        {
            var index = CursorIndex + i;
            index = Round(index);
            Buffer.Add(index);
        }

        for (var i = 0; i < BufferCount; ++i)
        {
            var index = CursorIndex + VisibleCount + i;
            index = Round(index);
            Buffer.Add(index);
        }

        //Debug.Log("Sync: " + this);
    }

    public void Reset()
    {
        CursorIndex = 0;
        Sync();
    }

    public override string ToString()
    {
        var buffer = GetInformation(Buffer);

        var result = string.Format("Buffer-{0}\nIndex-{1}", buffer, CursorIndex);
        return result;
    }

    private string GetInformation(List<int> list)
    {
        var result = string.Empty;
        list.ForEach(item => result += string.Format(" {0}", item));
        return string.Format("[ {0} ]", result);
    }
    
    void Awake()
    {
        totalCount = StripContainer.StripList.Count;
        Buffer = new List<int>(totalCount);

        Reset();
    }
}
