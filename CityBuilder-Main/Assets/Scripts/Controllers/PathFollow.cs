using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class PathFollow
{
    public PathCreator PathCreator { get; set; }
    public Transform Transform { get; set; }
    public float Speed { get; set; }
    public float DistanceTraveled { get; set; }
    public Transform Follower { get; set; }
    public object Path { get; set; }
    public PathType PathType { get; set; }
    public bool Forwards { get; set; }


    public PathFollow(
        Transform transform,
        object path,
        PathType type,
        Transform follower,
        bool forwards = true)
    {
        PathCreator = GlobalSettings.PathCreator;
        Debug.Log(GlobalSettings.PathCreator == null);
        Transform = transform;
        DistanceTraveled = 0;
        PathType = type;
        Forwards = forwards;

        switch (type)
        {
            case PathType.road:
                Speed = GlobalSettings.SpeedLimit;
                Follower = follower;
                Path = (Road)path;
                break;
        }
    }
    public void FollowPath()
    {
        Vector3 offset = PathCreator.path.GetNormalAtDistance(DistanceTraveled) * GlobalSettings.LaneOffset * (Forwards ? -1 : 1);

        DistanceTraveled += Speed * Time.deltaTime * (Forwards ? -1 : 1);
        Transform.position = PathCreator.path.GetPointAtDistance(DistanceTraveled) + offset;
        Transform.rotation = PathCreator.path.GetRotationAtDistance(DistanceTraveled)*Quaternion.Euler(0, 0, 90);
    }
}

public enum PathType
{
    road = 0,
    water = 1,
    walkway = 2
}