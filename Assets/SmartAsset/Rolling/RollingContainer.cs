using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingContainer : MonoBehaviour
{
    public GameObject TemplateObject;
    public RollingStrip RollingStrip;
    public float Speed;

    public ColliderCombiner Combiner;
    public RollingItemUpdater RollerUpdater;

    private List<GameObject> stripObjectList;
    private float unitHeight;
    private int totalNum;
    private SpinDirection direction;
    private Vector3 standardVec;

    public void Spin(int count)
    {
        StartCoroutine(DoSpin(count));
    }

    IEnumerator DoSpin(int count)
    {
        var bufferCount = RollingStrip.BufferCount;
        var stepCount = count / bufferCount;
        var finalStep = count % bufferCount;

        //var counter = 0;
        while (stepCount > 0)
        {
            yield return StartCoroutine(DoSpinStep(bufferCount));
            --stepCount;

            //Debug.Log("********* Spin round " + (counter++) + " current frame: " + Time.frameCount);
        }
        yield return StartCoroutine(DoSpinStep(finalStep));

        PositionTunning();

        Debug.Log("----------------- final Spin round with count: " + count + " done." + ", in frame: " + Time.frameCount + ", name: " + name);
    }

    public void SpinForever()
    {
        StartCoroutine(DoSpinForever());
    }

    IEnumerator DoSpinForever()
    {
        var bufferCount = RollingStrip.BufferCount;
        while (true)
        {
            yield return StartCoroutine(DoSpinStep(bufferCount));

            Debug.Log("*********** Spin round done.");
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator DoSpinStep(int step)
    {
        var stepVec = new Vector3(0, unitHeight * step, 0);
        var startPosition = stripObjectList[0].transform.position;
        var endPosition = (direction == SpinDirection.Down)
                              ? standardVec - stepVec
                              : standardVec + stepVec;
        var beginPosition = startPosition;

        //Debug.Log("Spin start position: " + startPosition + ", spin end position: " + endPosition);

        do
        {
            // rolling update.
            var increament = Time.deltaTime*Speed;
            increament = (direction == SpinDirection.Down) ? -increament : increament;
            var incrementVec = new Vector3(0, increament, 0);
            beginPosition += incrementVec;

            //Debug.Log("beginPosition: " + beginPosition + ", endPosition: " + endPosition);

            if((direction == SpinDirection.Down && beginPosition.y <= endPosition.y) || (direction == SpinDirection.Up && beginPosition.y >= endPosition.y))
            {
                beginPosition -= incrementVec;
                break;
            }

            UpdatePosition(incrementVec);

            yield return null;
        } while (true);

        var delta = beginPosition - endPosition;
        RePosition(step, delta);
        UpdateData(step);
        UpdateUI();
    }

    private void PositionTunning()
    {
        for (var i = 0; i < totalNum; ++i)
        {
            stripObjectList[i].transform.position = new Vector3(transform.position.x, transform.position.y - i * unitHeight, transform.position.z);
        }
    }

    private void UpdatePosition(Vector3 incrementVec)
    {
        stripObjectList.ForEach(item => item.transform.position += incrementVec);
        //Debug.Log("UpdatePosition in frame: " + Time.frameCount + ", name: " + name + ", 0's before position: " + stripObjectList[0].transform.position);
    }

    private void UpdateData(int step)
    {
        RollingStrip.Next(step);
    }

    private void UpdateUI()
    {
        for (var i = 0; i < totalNum; ++i)
        {
            RollerUpdater.Change(stripObjectList[i], RollingStrip, i);
        }
    }

    private void RePosition(int step, Vector3 delta)
    {
        var incrementY = (direction == SpinDirection.Down) ? unitHeight * step : -unitHeight * step;
        var incrementVec = new Vector3(0, incrementY, 0) + delta;

        foreach (var stripObject in stripObjectList)
        {
            stripObject.transform.position += incrementVec;
        }
        //Debug.Log("RePosition in frame: " + Time.frameCount + ", name: " + name + ", 0's before position: " + stripObjectList[0].transform.position);
    }
    
    public void Generate()
    {
        Reset();

        IntializeChildren();

        GenerateBounds();
    }

    private void GenerateBounds()
    {
        Combiner.CombineList.AddRange(stripObjectList);
        Combiner.CombineBounds();
    }

    private void IntializeChildren()
    {
        for(var i = 0; i < totalNum; ++i)
        {
            var item = stripObjectList[i];
            item.transform.position = new Vector3(transform.position.x, transform.position.y - unitHeight * i,
                                                  transform.position.z);
            RollerUpdater.Change(stripObjectList[i], RollingStrip, i);
        }
    }

    public void Reset()
    {
        Debug.Log("Reset, " + name);

        totalNum = RollingStrip.BufferCount * 2 + RollingStrip.VisibleCount;
        direction = RollingStrip.Direction;
        standardVec = transform.position;

        //Debug.Log("Standard vector: " + standardVec);

        var boxCollider = TemplateObject.collider as BoxCollider;
        unitHeight = boxCollider.size.y * TemplateObject.transform.localScale.y;

        //Debug.Log("Unit height: " + unitHeight);

        RollingStrip.Reset();
    }

    public void Initialize()
    {
        totalNum = RollingStrip.BufferCount * 2 + RollingStrip.VisibleCount;
        stripObjectList = new List<GameObject>(totalNum);
        for (var i = 0; i < totalNum; ++i)
        {
            var item = Instantiate(TemplateObject) as GameObject;
            item.transform.parent = transform;
            item.layer = gameObject.layer;
            item.name = string.Format("{0}_{1}", item.name, (i + 1));
            stripObjectList.Add(item);
        }
    }

    void Awake()
    {
        Validate();

        Initialize();
    }

    void Start()
    {
        Generate();
    }

    private void Validate()
    {
        if (TemplateObject == null)
        {
            Debug.LogError("Template object should not be null.");
        }

        var boxCollider = TemplateObject.collider as BoxCollider;
        if (boxCollider == null)
        {
            Debug.LogError("Please attach box collider to template object " + TemplateObject.name);
        }
    }
}
