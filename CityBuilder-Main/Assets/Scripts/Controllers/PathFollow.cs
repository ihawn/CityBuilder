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
    public object Follower { get; set; }
    public PathType PathType { get; set; }

    public PathFollow(Transform transform, PathType type)
    {
        PathCreator = GlobalSettings.PathCreator;
        Transform = transform;
        DistanceTraveled = 0;
        PathType = type;

        switch (type)
        {
            case PathType.road:
                Speed = GlobalSettings.ResidentialRoadSpeed;
                break;
        }
    }
    public void FollowPath()
    {
        DistanceTraveled -= Speed * Time.deltaTime;
        Transform.position = PathCreator.path.GetPointAtDistance(DistanceTraveled);
        Transform.rotation = PathCreator.path.GetRotationAtDistance(DistanceTraveled)*Quaternion.Euler(0, 0, 90);
    }
}

public enum PathType
{
    road = 0,
    water = 1,
    walkway = 2
}