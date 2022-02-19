using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scene Objects")]
    public PathInit PathInit;
    public VehicleInit VehicleInit;
    public GlobalSettings GlobalSettings;
    public GameObject Paths;
    public DebugMethods DebugMethods;

    [Header("Transportation Lists")]
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public List<Node> Nodes = new List<Node>();

    void Awake()
    {
        GlobalSettings.UpdateParameters();
    }

    void Start()
    {
        PathInit.Init();
        VehicleInit.Init();
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
