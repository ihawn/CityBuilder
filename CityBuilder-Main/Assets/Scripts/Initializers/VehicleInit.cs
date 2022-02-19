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
            PathFollow pf = new PathFollow(gm.Nodes, PathType.road);
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
