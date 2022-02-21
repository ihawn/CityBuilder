using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Linq;

public class VehicleController : MonoBehaviour
{
    public Vehicle vehicle;
    public PathFollow pf;
    public int id;
    public float curveMagnitude; //Multiplier which will make agent travel slower the tighter the turn is

    public void FollowPath()
    {
        VertexPath path = pf.PathCreator == null ? null : pf.PathCreator.path;
        if (path != null)
        {
            Vector3 offset = path.GetNormalAtDistance(pf.DistanceTraveled) * GlobalSettings.LaneOffset * (pf.Forwards ? 1 : -1);
            pf.DistanceTraveled += pf.Speed * Time.deltaTime * (pf.Forwards ? 1 : -1);
            transform.position = path.GetPointAtDistance(pf.DistanceTraveled) + offset;
            transform.rotation = path.GetRotationAtDistance(pf.DistanceTraveled) * Quaternion.Euler(pf.Forwards ? 180 : 0, 0, 90);

            CheckForDestination(path);
        }
    }
    public void UpdateTravelParameters()
    {
        curveMagnitude = pf.GetTurnTightnessAtDistance(pf.DistanceTraveled);

        pf.Speed += pf.Acceleration * Time.deltaTime * curveMagnitude;
        pf.Speed = Mathf.Clamp(pf.Speed, 0, pf.SpeedLimit);
    }


    void CheckForDestination(VertexPath path)
    {
        float distFromEnd = Vector3.Distance(transform.position, path.GetPointAtDistance(path.length));
        if(distFromEnd < 2*pf.Speed*Time.deltaTime)
        {
            Debug.Log("Destination reached");
            SetRandomPathFromCurrentNode();
        }
    }

    void SetRandomPathFromCurrentNode()
    {
        GameManager gm = GlobalSettings.GameManager;
        List<Node> possibleDestinations = gm.Nodes.Where(x => x.Id != pf.DestinationNodeId).ToList();
        int nextId = possibleDestinations[Random.Range(0, possibleDestinations.Count)].Id;
        SetNextDestination(nextId);
    }

    void SetNextDestination(int nodeId)
    {
        pf.SetPath(pf.DestinationNodeId.Value, nodeId);
        pf.StartNodeId = pf.DestinationNodeId.Value;
        pf.DestinationNodeId = nodeId;
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
