using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;

public class Road
{
    public List<RoadPiece> RoadPieces { get; set; }
    public List<Transform> BezierPoints { get; set; }
    public VertexPath VertexPath { get; set; }
    public Transform Start { get; set; }
    public RoadProperties Properties { get; set; }
    public int Id { get; set; }

    public Road(GameObject road, RoadType type)
    {
        Id = Properties.Id;
        RoadPieces = new List<RoadPiece>();
        BezierPoints = new List<Transform>();

        if(type == RoadType.road)
        {
            Start = road.transform.GetChild(0);
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
        }
        else if(type == RoadType.intersection)
        {
            foreach(Transform t in road.transform)
                BezierPoints.Add(t);
        }

        var Path = new BezierPath(BezierPoints.ToArray(), false, PathSpace.xz);
        VertexPath = new VertexPath(Path, RoadPieces[0].ObjectSettings.GameObject.transform);
    }
}

public enum RoadType
{
    road = 0,
    intersection = 1
}
