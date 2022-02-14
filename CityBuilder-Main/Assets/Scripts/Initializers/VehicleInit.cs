using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInit : MonoBehaviour
{
    public List<GameObject> VehicleGameObjects;
    
    public void Init(GameManager gm)
    {
        int id = 0;
        foreach (GameObject g in VehicleGameObjects)
        {
            Vehicle v = new Vehicle(VehicleType.car, g, gm.Roads[0]);
            var vc = g.GetComponent<VehicleController>();
            vc.vehicle = v;
            vc.id = id;
            gm.Vehicles.Add(v);
            id++;
        }
    }
}
