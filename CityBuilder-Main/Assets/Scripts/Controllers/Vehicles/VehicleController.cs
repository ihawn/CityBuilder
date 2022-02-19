using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class VehicleController : MonoBehaviour
{
    public Vehicle vehicle;
    public PathCreator creator;
    public PathFollow pf;
    public int id;

    public void FollowPath()
    {
        VertexPath path = pf.PathCreator.path;
        Vector3 offset = path.GetNormalAtDistance(pf.DistanceTraveled) * GlobalSettings.LaneOffset * (pf.Forwards ? -1 : 1);
        pf.DistanceTraveled += pf.Speed * Time.deltaTime * (pf.Forwards ? -1 : 1);
        transform.position = path.GetPointAtDistance(pf.DistanceTraveled) + offset;
        transform.rotation = path.GetRotationAtDistance(pf.DistanceTraveled) * Quaternion.Euler(pf.Forwards ? 0 : 180, 0, 90);
    }
}
