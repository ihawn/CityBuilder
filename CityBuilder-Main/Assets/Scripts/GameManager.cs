using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public RoadInit RoadInit;
    public VehicleInit VehicleInit;
    public GlobalSettings GlobalSettings;
    public GameObject Paths;
    public DebugMethods DebugMethods;

    [Header("Transportation Lists")]
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public List<Road> Roads = new List<Road>();
    public List<Intersection> Intersections = new List<Intersection>();

    void Awake()
    {
        GlobalSettings.UpdateParameters();
    }

    void Start()
    {
        RoadInit.Init(this);
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
            vehicle.VehicleController.FollowPath();
        }
    }
}
