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
        transform.rotation = path.GetRotationAtDistance(pf.DistanceTraveled) * Quaternion.Euler(0, 0, 90);
    }

    bool ShouldFlip(Road roadEntered, Vector3 triggerPos, PathFollow pf)
    {
        float d1 = Vector3.Distance(roadEntered.Start.position, triggerPos);
        float d2 = Vector3.Distance(roadEntered.End.position, triggerPos);
        return (d1 < d2 && pf.Forwards) || (d1 > d2 && !pf.Forwards);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "PathFork":
                var ip = other.transform.parent.GetComponent<IntersectionProperties>();
                Intersection i = GlobalSettings.GameManager.Intersections[ip.Id];

                Debug.Log("intersection");
                int numTurns = i.Roads.Count;
                int choice = Random.Range(0, numTurns);
                Road newFollow = i.Roads[choice];
                pf.RoadFollowed = GlobalSettings.GameManager.Roads[newFollow.Id];
                pf.PathCreator = pf.RoadFollowed.PathCreator;

                bool shouldFlip = ShouldFlip(pf.RoadFollowed, other.transform.position, pf);
                pf.DistanceTraveled = shouldFlip ? pf.PathCreator.path.length : 0;
                pf.Forwards = shouldFlip ? !pf.Forwards : pf.Forwards;
                break;

            case "ForkEnd":
                Debug.Log("intersection");
                var rp = other.transform.parent.GetComponent<RoadProperties>();
                pf.RoadFollowed = GlobalSettings.GameManager.Roads[rp.Id];
                pf.PathCreator = pf.RoadFollowed.PathCreator;

                bool shouldFlip2 = ShouldFlip(pf.RoadFollowed, other.transform.position, pf);
                pf.DistanceTraveled = shouldFlip2 ? pf.PathCreator.path.length : 0;
                pf.Forwards = shouldFlip2 ? !pf.Forwards : pf.Forwards;
                break;
        }
    }
}
