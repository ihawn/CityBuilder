using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Road
{
    public List<RoadPiece> RoadPieces { get; set; }
    public List<Transform> BezierPoints { get; set; }
    public VertexPath VertexPath { get; set; }
    public Transform Start { get; set; }

    public Road(GameObject road)
    {
        RoadPieces = new List<RoadPiece>();
        BezierPoints = new List<Transform>();
        Start = road.transform.GetChild(0);
        Transform lastPoint = Start;
        foreach(Transform piece in road.transform.GetChild(1).transform)
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

        var Path = new BezierPath(BezierPoints.ToArray(), false, PathSpace.xz);
        VertexPath = new VertexPath(Path, RoadPieces[0].ObjectSettings.GameObject.transform);
    }
}
