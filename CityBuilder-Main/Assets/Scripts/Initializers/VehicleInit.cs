using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInit : MonoBehaviour
{
    public List<GameObject> VehicleGameObjects;
    public List<GameObject> RoadGameObjects;
    
    public void Init(GameManager gm)
    {
        //Road init
        foreach (GameObject g in RoadGameObjects)
        {
            Road r = new Road(g);
            gm.Roads.Add(r);
        }

        //Vehicle Init
        foreach (GameObject g in VehicleGameObjects)
        {
            Vehicle v = new Vehicle(VehicleType.car, g, gm.Roads[0]);
            gm.Vehicles.Add(v);
        }
    }
}
