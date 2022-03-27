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
    public float distanceFromLastSlowdownTrail;
    public int slowzoneObjectsCount = 0;

    public void FollowPath()
    {
        VertexPath path = pf.PathCreator == null ? null : pf.PathCreator.path;
        if (path != null)
        {
            Vector3 offset = path.GetNormalAtDistance(pf.DistanceTraveled) * GlobalSettings.LaneOffset * (pf.Forwards ? 1 : -1);
            pf.DistanceTraveled += pf.Speed * Time.deltaTime * (pf.Forwards ? 1 : -1);
            transform.position = path.GetPointAtDistance(pf.DistanceTraveled) + offset;
            transform.rotation = path.GetRotationAtDistance(pf.DistanceTraveled) * Quaternion.Euler(pf.Forwards ? 180 : 0, 0, 90);

            UpdateSlowdownTrail();
            UpdateAcceleration();
            CheckForDestination(path);
        }
    }

    public void UpdateTravelParameters()
    {
        curveMagnitude = pf.GetTurnTightnessAtDistance(pf.DistanceTraveled);

        pf.Speed += pf.Acceleration * Time.deltaTime * curveMagnitude;
        pf.Speed = Mathf.Clamp(pf.Speed, 0, pf.SpeedLimit);
    }

    void UpdateSlowdownTrail()
    {
        distanceFromLastSlowdownTrail += pf.Speed * Time.deltaTime;
        if (distanceFromLastSlowdownTrail >= GlobalSettings.SlowdownTrailDropOffset)
        {
            distanceFromLastSlowdownTrail = 0;
            MakeSlowdownTrail();
        }
    }

    void MakeSlowdownTrail()
    {
        PoolingAgent pa = vehicle.PoolingAgents["SlowdownPooler"];
        GameObject slowdownObject = pa.GetPooledObject();
        slowdownObject.transform.position = transform.position -
            transform.forward * 2 * GlobalSettings.SlowdownTrailDropOffset * (pf.Forwards ? 1 : -1);

        slowdownObject.SetActive(true);

        DestroyAfterDistanceFromObject des = slowdownObject.GetComponent<DestroyAfterDistanceFromObject>();
        des.obj = gameObject;
        des.distance = GlobalSettings.SlowdownTrailDropOffset*5;
        des.deactivate = true;
    }

    void CheckForDestination(VertexPath path)
    {
        float distFromEnd = Vector3.Distance(transform.position, path.GetPointAtDistance(path.length));
        if(distFromEnd < 2*pf.Speed*Time.deltaTime)
        {
            SetRandomPathFromCurrentNode();
        }
    }

    void SetRandomPathFromCurrentNode()
    {
        GameManager gm = GlobalSettings.GameManager;
        List<Node> possibleDestinations = gm.Nodes.Where(x => x.Id != pf.DestinationNodeId).ToList();
        int nextId = possibleDestinations[Random.Range(0, possibleDestinations.Count)].Id;

        SetNextDestination(nextId, fromRandom: true);
    }

    void SetNextDestination(int nodeId, bool fromRandom = false)
    {
        pf.SetPath(pf.DestinationNodeId.Value, nodeId, fromRandom);
        pf.StartNodeId = pf.DestinationNodeId.Value;
        pf.DestinationNodeId = nodeId;
    }

    void UpdateAcceleration()
    {
        slowzoneObjectsCount = Mathf.Max(slowzoneObjectsCount, 0);
        if(slowzoneObjectsCount == 0)
        {
            pf.Acceleration = pf.SpeedAcceleration;
            pf.Slowing = false;
        }
        else
        {
            if(vehicle.Obstacle == null)
                pf.Acceleration = pf.SlowAcceleration;
            else
            {
                float dist = Vector3.Distance(vehicle.Obstacle.transform.position, transform.position);
                pf.Acceleration = pf.SlowAcceleration * GlobalSettings.StartSlowdownThreshold / Mathf.Max(Mathf.Abs(dist - GlobalSettings.StartSlowdownThreshold), 0.01f);
            }
            pf.Slowing = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "SlowZone":
                slowzoneObjectsCount++;
                break;
            case "ObstacleSlowZone":
                slowzoneObjectsCount++;
                vehicle.Obstacle = other.GetComponent<DestroyAfterDistanceFromObject>().obj;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "SlowZone":
                slowzoneObjectsCount--;
                break;
            case "ObstacleSlowZone":
                slowzoneObjectsCount--;
                vehicle.Obstacle = null;
                break;
        }
    }
}
