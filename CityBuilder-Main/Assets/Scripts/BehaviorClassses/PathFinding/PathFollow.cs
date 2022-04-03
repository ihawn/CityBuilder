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
    public object FollowerController { get; set; }
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
    public GameObject Detector { get; set; }
    public float DetectorLength { get; }

    public PathFollow(
        PathType type,
        int startNodeId,
        GameObject followerObject,
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
                FollowerObject = followerObject;
                FollowerController = followerObject.GetComponent<VehicleController>();
                DetectorLength = GlobalSettings.DetectorLength;
                MakeDetector();
                Speed = destinationNodeId == null ? 0 : GlobalSettings.SpeedLimit;
                SpeedLimit = speedLimit == null ? GlobalSettings.SpeedLimit : speedLimit.Value;
                SpeedAcceleration = speedAcceleration == null ? GlobalSettings.SpeedAccelerationVehicle : speedAcceleration.Value;
                SlowAcceleration = slowAcceleration == null ? GlobalSettings.SlowAccelerationVehicle : slowAcceleration.Value;
                break;
        }
    }

    public void SetPath(int startNodeId, int? endNodeId, bool fromRandom = false)
    {
        var nodes = GlobalSettings.GameManager.Nodes;

        DistanceTraveled = 0;
        StartNodeId = startNodeId;
        DestinationNodeId = endNodeId;

        Path = endNodeId == null ? null : AStar.GetShortestPath(nodes[startNodeId], nodes[endNodeId.Value], nodes);

        //Prevent generating a random path from returning a 1 point path
        //Will cause car to teleport
        if (endNodeId != null && fromRandom)
        {
            while (Path.Count < 3)
            {
                Path = AStar.GetShortestPath(nodes[Random.Range(0, nodes.Count)], nodes[Random.Range(0, nodes.Count)], nodes);
            }
        }

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

    //Returns a float between 0, 1 | 1 = straight road, 0 = no straightness
    public float GetTurnTightnessAtDistance(float dist)
    {
        float offset = 0.5f;
        bool b = dist + offset > PathCreator.path.length;
        var n1 = PathCreator.path.GetNormalAtDistance(dist + (b ? 0 : offset));
        var n2 = PathCreator.path.GetNormalAtDistance(dist - (b ? offset : 0));
        return Mathf.Clamp(1 - Vector3.Angle(n1, n2)/45, 0.4f, 1f);
    }

    public void MakeDetector()
    {
        GameObject g = new GameObject();
        g.name = "Detector";
        g.AddComponent<BoxCollider>();
        g.GetComponent<BoxCollider>().isTrigger = true;
        g.GetComponent<BoxCollider>().size = new Vector3(GlobalSettings.DetectorWidth, GlobalSettings.DetectorWidth, GlobalSettings.DetectorLength);
        g.AddComponent<DetectorController>();
        g.GetComponent<DetectorController>().ParentPathFollowController = FollowerController;
        g.AddComponent<Rigidbody>();
        g.GetComponent<Rigidbody>().isKinematic = true;
        g.GetComponent<Rigidbody>().useGravity = false;
        g.tag = "Detector";
        g.transform.parent = GlobalSettings.GameManager.DetectorContainer.transform;

        Detector = g;
    }

    public void SetDetectorPosition()
    {
        float objectLength = Vector3.Magnitude(FollowerObject.GetComponent<MeshRenderer>().bounds.size);
        float forwardsOffset = -(objectLength + DetectorLength)/2;
        Detector.transform.position = PathCreator.path.GetPointAtDistance(DistanceTraveled + forwardsOffset);
        Detector.transform.rotation = PathCreator.path.GetRotationAtDistance(DistanceTraveled + forwardsOffset);
    }

    public bool IsAheadOf(PathFollow otherPf)
    {
        return Vector3.Dot(otherPf.Detector.transform.position - Detector.transform.position, otherPf.Detector.transform.forward) >= 0;
    }
}

public enum PathType
{
    road = 0,
    water = 1,
    walkway = 2
}