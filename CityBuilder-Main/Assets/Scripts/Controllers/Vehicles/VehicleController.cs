using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class VehicleController : MonoBehaviour
{
    public Vehicle vehicle;
    public PathFollow pf;
    public int id;

    public void FollowPath()
    {
        VertexPath path = pf.PathCreator.path;
        Vector3 offset = path.GetNormalAtDistance(pf.DistanceTraveled) * GlobalSettings.LaneOffset * (pf.Forwards ? 1 : -1);
        pf.DistanceTraveled += pf.Speed * Time.deltaTime * (pf.Forwards ? 1 : -1);
        transform.position = path.GetPointAtDistance(pf.DistanceTraveled) + offset;
        transform.rotation = path.GetRotationAtDistance(pf.DistanceTraveled) * Quaternion.Euler(pf.Forwards ? 180 : 0, 0, 90);
    }

    public void UpdateSpeed()
    {
        pf.Speed += pf.Acceleration * Time.deltaTime;
        pf.Speed = Mathf.Clamp(pf.Speed, 0, pf.SpeedLimit);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "SlowZone":
                pf.Acceleration = pf.SlowAcceleration;
                pf.Slowing = false;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "SlowZone":
                pf.Acceleration = pf.SpeedAcceleration;
                pf.Slowing = true;
                break;
        }
    }
}
