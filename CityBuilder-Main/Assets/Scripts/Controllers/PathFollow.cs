using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class PathFollow
{
    public PathCreator PathCreator { get; set; }
    public GameObject FollowerObject { get; set; }
    public Transform Transform { get; set; }
    public float Speed { get; set; }
    public float DistanceTraveled { get; set; }
    public PathType PathType { get; set; }
    public bool Forwards { get; set; }
    public Road RoadFollowed { get; set; }

    public PathFollow(
        GameObject g,
        PathType type,
        bool forwards = true,
        Road roadFollowed = null)
    {
        Transform = g.transform;
        FollowerObject = g;
        DistanceTraveled = 0;
        PathType = type;
        Forwards = forwards;
        RoadFollowed = roadFollowed;
        PathCreator = roadFollowed.PathCreator;

        switch (type)
        {
            case PathType.road:
                Speed = GlobalSettings.SpeedLimit;
                break;
        }
    }
}

public enum PathType
{
    road = 0,
    water = 1,
    walkway = 2
}