using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathCreatorController : MonoBehaviour
{
    public PathCreator DrawPath(BezierPath bp)
    {
        GameObject creatorContainer = new GameObject("Bezier Path");
        creatorContainer.AddComponent<PathCreator>();
        PathCreator creator = creatorContainer.GetComponent<PathCreator>();
        creator.bezierPath = bp;
        creatorContainer.transform.parent = GlobalSettings.GameManager.Paths.transform;

        return creator;
    }
}
