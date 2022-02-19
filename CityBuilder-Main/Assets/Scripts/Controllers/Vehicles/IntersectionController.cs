using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public Intersection Intersection;
    public IntersectionState State;
    public float cycleTimer;
    public float nextTime;
    public IDictionary<IntersectionState, Color> LightColors = new Dictionary<IntersectionState, Color>()
    {
        { IntersectionState.straight, Color.green },
        { IntersectionState.right, Color.blue },
        { IntersectionState.orthogonal, Color.red },
        { IntersectionState.left, Color.cyan }
    };

    public void UpdateIntersections()
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

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", LightColors[Intersection.State]);
        }
    }
}
