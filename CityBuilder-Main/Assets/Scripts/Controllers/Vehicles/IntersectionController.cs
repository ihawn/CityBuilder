using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public Intersection Intersection;
    public IntersectionState State;
    public float cycleTimer;
    public float nextTime;
    float slowZoneOffset = -2;
    public IDictionary<IntersectionState, Color> LightColors = new Dictionary<IntersectionState, Color>()
    {
        { IntersectionState.straight, Color.green },
        { IntersectionState.right, Color.blue },
        { IntersectionState.orthogonal, Color.red },
        { IntersectionState.left, Color.cyan }
    };

    public void UpdateIntersection()
    {
        Intersection.CycleTimer += Time.deltaTime;
        State = Intersection.State;
        cycleTimer = Intersection.CycleTimer;
        nextTime = Intersection.NextTime;

        //State change
        if(Intersection.CycleTimer >= Intersection.NextTime)
        {
            int stateOffset = 1;
            if (Intersection.State == IntersectionState.left)
                stateOffset = -3;
            else if (Intersection.State == IntersectionState.right && Intersection.PathCount == 3)
                stateOffset = 2;
            Intersection.State += stateOffset;
            Intersection.NextTime = Intersection.Timings[Intersection.State];
            Intersection.CycleTimer = 0;

            SetIntersectionState(Intersection.State);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", LightColors[Intersection.State]);
        }
    }

    public void SetIntersectionState(IntersectionState state)
    {
        switch (state)
        {
            case IntersectionState.straight:
                SetIntersectionChildState(false);
                break;

            case IntersectionState.left:
                SetIntersectionChildState(false);
                break;

            case IntersectionState.right:
                SetIntersectionChildState(true);
                break;

            case IntersectionState.orthogonal:
                SetIntersectionChildState(true);
                break;
        }
    }

    void SetIntersectionChildState(bool b)
    {
        float activeHeight = slowZoneOffset;
        float inactiveHeight = 10;

        int i = 0;
        foreach(Transform t in transform)
        {
            if(t.name.ToLower().Contains("triggerzone"))
            {
                Vector3 pos;
                if (i == 0 || i == 1) { pos = new Vector3(t.localPosition.x, b ? activeHeight : inactiveHeight, t.localPosition.z); }
                else { pos = new Vector3(t.localPosition.x, !b ? activeHeight : inactiveHeight, t.localPosition.z); }
                t.localPosition = pos;
                i++;
            }
        }
    }
}
