using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInit : MonoBehaviour
{
    public List<GameObject> VehicleGameObjects;
    
    public void Init()
    {
        int id = 0;
        GameManager gm = GlobalSettings.GameManager;

        foreach (GameObject g in VehicleGameObjects)
        {
            List<Node> nodeCopy = new List<Node>(gm.Nodes);
            List<Node> path = AStar.GetShortestPath(nodeCopy[0], nodeCopy[10], nodeCopy);
            Debug.Log(path.Count);
            PathFollow pf = new PathFollow(path, PathType.road);
            Vehicle v = new Vehicle(VehicleType.car, g, pf);
            var vc = g.GetComponent<VehicleController>();
            vc.vehicle = v;
            vc.id = id;
            vc.pf = v.PathFollow;
            gm.Vehicles.Add(v);
            id++;
        }
    }
}
