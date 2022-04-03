using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Central Objects")]
    public GlobalSettings GlobalSettings;
    public GameObject Paths;
    public PathCreatorController DebugMethods;
    public PoolerInit PoolerInit;
    public GameObject DetectorContainer;

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

    [Header("Camera Objects")]
    public PanAndZoom PanAndZoom;

    void Awake()
    {
        GlobalSettings.UpdateParameters();
    }

    void Start()
    {
        PathInit.Init();
        VehicleInit.Init();
        IntersectionInit.Init();
        PanAndZoom.Init();
        PoolerInit.Init();
    }

    void Update()
    {
        GlobalSettings.UpdateParameters();
        //PanAndZoom.UpdateCameraTransform();
        ControllerBase.UpdateControllers();
    }
}
