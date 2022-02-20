using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection
{
    public IntersectionState State { get; set; }
    public Dictionary<IntersectionState, float> Timings { get; set; }
    public IntersectionController IntersectionController { get; set; }
    public List<GameObject> SlowZones { get; set; }
    public float CycleTimer { get; set; }
    public float NextTime { get; set; }
    public int PathCount { get; set; }
    public int Id { get; set; }

    public Intersection(int pathCount, IntersectionState state = IntersectionState.straight, Dictionary<IntersectionState, float> timings = null)
    {
        State = state;
        PathCount = pathCount;
        Timings = timings == null ? GlobalSettings.IntersectionTimings : timings;
        SlowZones = new List<GameObject>();
        CycleTimer = 0;
        NextTime = Timings[state];
    }
}

public enum IntersectionState
{
    straight = 0,
    right = 1,
    orthogonal = 2,
    left = 3,
}
