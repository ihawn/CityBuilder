using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Central Objects")]
    public GlobalSettings GlobalSettings;
    public GameObject Paths;
    public PathCreatorController DebugMethods;

    [Header("Initializers")]
    public PathInit PathInit;
    public VehicleInit VehicleInit;
    public IntersectionInit IntersectionInit;

    [Header("Vehicle Lists")]
    public List<Vehicle> Vehicles = new List<Vehicle>();

    [Header("Road Objects")]
    public List<Intersection> Intersections = new List<Intersection>();

    [Header("Pathfinding Lists")]
    public List<Node> Nodes = new List<Node>();

    void Awake()
    {
        GlobalSettings.UpdateParameters();
    }

    void Start()
    {
        PathInit.Init();
        VehicleInit.Init();
        IntersectionInit.Init();
        
    }

    void Update()
    {
        GlobalSettings.UpdateParameters();
        ControllerBase.UpdateControllers();
    }
}
