using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public VehicleInit VehicleInit;

    [Header("Vehicle Lists")]
    public List<Vehicle> Vehicles = new List<Vehicle>();

    void Start()
    {
        VehicleInit.Init(this);
    }

    void Update()
    {
        UpdateVehicles();
    }

    void UpdateVehicles()
    {
        foreach (var vehicle in Vehicles)
        {
            vehicle.PathFollow.FollowPath();
        }
    }
}
