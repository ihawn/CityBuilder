using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Road
{
    public List<RoadPiece> RoadPieces { get; set; }
    public List<Vector3> BezierPoints { get; set; }
    public PathCreator PathCreator { get; set; }
    public VertexPath VertexPath { get; set; }

    public Road(GameObject road, PathCreator pathCreator)
    {
        RoadPieces = new List<RoadPiece>();
        BezierPoints = new List<Vector3>();
        foreach(Transform piece in road.transform)
        {
            RoadPiece r = new RoadPiece(piece.gameObject);
            RoadPieces.Add(r);
            BezierPoints.Add(piece.position);
        }

        var Path = new BezierPath(BezierPoints.ToArray(), false, PathSpace.xz);
        PathCreator = pathCreator;
        PathCreator.bezierPath = Path;
        VertexPath = new VertexPath(Path, RoadPieces[0].ObjectSettings.GameObject.transform);
    }
}
