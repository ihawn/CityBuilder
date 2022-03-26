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
            PathFollow pf = new PathFollow(PathType.road, Random.Range(0,250), destinationNodeId: Random.Range(251, 360));
            pf.FollowerObject = g;
            Vehicle v = new Vehicle(VehicleType.car, g, pf, gm.PoolerInit.Poolers[0]);
            var vc = g.GetComponent<VehicleController>();
            g.transform.position = gm.Nodes[204].Position;
            vc.vehicle = v;
            vc.id = id;
            vc.pf = v.PathFollow;
            gm.Vehicles.Add(v);
            id++;
        }
    }
}
