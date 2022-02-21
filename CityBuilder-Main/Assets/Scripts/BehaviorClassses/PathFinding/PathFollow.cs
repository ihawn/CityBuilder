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
    public int? DestinationNodeId { get; set; }
    public int StartNodeId { get; set; }
    public List<Node> Path { get; set; }

    public PathFollow(
        PathType type,
        int startNodeId,
        float? speedLimit = null,
        float? speedAcceleration = null,
        float? slowAcceleration = null,
        int? destinationNodeId = null)
    {
        Acceleration = 0;

        SetPath(startNodeId, destinationNodeId);

        switch (type)
        {
            case PathType.road:
                Speed = destinationNodeId == null ? 0 : GlobalSettings.SpeedLimit;
                SpeedLimit = speedLimit == null ? GlobalSettings.SpeedLimit : speedLimit.Value;
                SpeedAcceleration = speedAcceleration == null ? GlobalSettings.SpeedAccelerationVehicle : speedAcceleration.Value;
                SlowAcceleration = slowAcceleration == null ? GlobalSettings.SlowAccelerationVehicle : slowAcceleration.Value;
                break;
        }
    }

    public void SetPath(int startNodeId, int? endNodeId)
    {
        var nodes = GlobalSettings.GameManager.Nodes;

        DistanceTraveled = 0;
        StartNodeId = startNodeId;
        DestinationNodeId = endNodeId;
        Path = endNodeId == null ? null : AStar.GetShortestPath(nodes[startNodeId], nodes[endNodeId.Value], nodes);

        if (Path != null)
        {
            List<Vector3> bezierPoints = Path.Select(x => x.Position).ToList();
            BezierPath path = new BezierPath(Vector3.zero, Vector3.zero, bezierPoints, false, PathSpace.xz);
            PathCreator creator = GlobalSettings.GameManager.DebugMethods.DrawPath(path);

            if (PathCreator != null)
                GameObject.Destroy(PathCreator);
            PathCreator = creator;
        }
    }
}

public enum PathType
{
    road = 0,
    water = 1,
    walkway = 2
}