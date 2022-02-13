using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInit : MonoBehaviour
{
    public List<GameObject> VehicleGameObjects;
    
    public void Init(GameManager gm)
    {
        foreach(GameObject g in VehicleGameObjects)
        {
            Vehicle v = new Vehicle(VehicleType.car, g);
            gm.Vehicles.Add(v);
        }
    }
}
