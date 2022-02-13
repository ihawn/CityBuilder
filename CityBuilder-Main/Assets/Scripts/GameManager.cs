using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public VehicleInit VehicleInit;
    public GlobalSettings GlobalSettings;

    [Header("Vehicle Lists")]
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public List<Road> Roads = new List<Road>();

    void Awake()
    {
        GlobalSettings.UpdateParameters();
    }

    void Start()
    {
        VehicleInit.Init(this);
    }

    void Update()
    {
        GlobalSettings.UpdateParameters();
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
