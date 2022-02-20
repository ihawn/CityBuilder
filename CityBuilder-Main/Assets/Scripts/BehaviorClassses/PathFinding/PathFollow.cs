using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;

[System.Serializable]
public class PathFollow
{
    public PathCreator PathCreator { get; set; }
    public GameObject FollowerObject { get; set; }
    public float Speed { get; set; }
    public float SpeedLimit { get; set; }
    public float DistanceTraveled { get; set; }
    public PathType PathType { get; set; }
    public bool Forwards { get; set; }
    public bool Slowing { get; set; }
    public float Acceleration { get; set; }
    public float SpeedAcceleration { get; set; }
    public float SlowAcceleration { get; set; }

    public PathFollow(List<Node> nodes,
        PathType type,
        float? speedLimit = null,
        float? speedAcceleration = null,
        float? slowAcceleration = null)
    {
        DistanceTraveled = 0;
        Acceleration = 0;

        List<Vector3> bezierPoints = nodes.Select(x => x.Position).ToList();
        BezierPath path = new BezierPath(Vector3.zero, Vector3.zero, bezierPoints, false, PathSpace.xz);
        PathCreator creator = GlobalSettings.GameManager.DebugMethods.DrawPath(path);
        PathCreator = creator;

        switch (type)
        {
            case PathType.road:
                Speed = GlobalSettings.SpeedLimit;
                SpeedLimit = speedLimit == null ? GlobalSettings.SpeedLimit : speedLimit.Value;
                SpeedAcceleration = speedAcceleration == null ? GlobalSettings.SpeedAccelerationVehicle : speedAcceleration.Value;
                SlowAcceleration = slowAcceleration == null ? GlobalSettings.SlowAccelerationVehicle : slowAcceleration.Value;
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