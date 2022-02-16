using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Road
{
    public List<RoadPiece> RoadPieces { get; set; }
    public List<Transform> BezierPoints { get; set; }
    public BezierPath BezierPath { get; set; }
    public VertexPath VertexPath { get; set; }
    public Transform Start { get; set; }
    public Transform End { get; set; }
    public RoadProperties Properties { get; set; }
    public PathCreator PathCreator { get; set; }
    public int Id { get; set; }
    public RoadType RoadType { get; set; }

    public Road(GameObject road, RoadType type, int id, Vector3 c1, Vector3 c2)
    {
        Id = id;
        RoadPieces = new List<RoadPiece>();
        BezierPoints = new List<Transform>();
        RoadType = type;

        Start = road.transform.GetChild(0);
        End = road.transform.GetChild(road.transform.childCount - 1);
        Transform lastPoint = Start;

        foreach (Transform piece in road.transform.GetChild(1).transform)
        {
            RoadPiece r = new RoadPiece(piece.gameObject);
            RoadPieces.Add(r);

            Transform bezierPoint1 = piece.GetChild(0);
            Transform bezierPoint2 = piece.GetChild(1);
            Transform p1 = Vector3.Distance(lastPoint.position, bezierPoint1.position) < Vector3.Distance(lastPoint.position, bezierPoint2.position) ? bezierPoint1 : bezierPoint2;
            Transform p2 = p1 == bezierPoint1 ? bezierPoint2 : bezierPoint1;
            BezierPoints.Add(p1);
            BezierPoints.Add(p2);
            lastPoint = p2;
        }

        var Path = new BezierPath(c1, c2, BezierPoints.ToArray(), false, PathSpace.xz);
        BezierPath = Path;
        VertexPath = new VertexPath(Path, RoadPieces[0].ObjectSettings.GameObject.transform);
        
        var creator = GlobalSettings.GameManager.DebugMethods.DrawPath(Path);
        PathCreator = creator;
    }
}

public enum RoadType
{
    road = 0,
    intersection = 1
}
