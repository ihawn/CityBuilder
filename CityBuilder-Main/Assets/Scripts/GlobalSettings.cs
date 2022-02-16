using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class GlobalSettings : MonoBehaviour
{
    [Header("Object References")]
    public GameManager gameManager;
    public static GameManager GameManager;

    [Header("Car Parameters")]
    public int carCapactiy = 4;
    public static int CarCapacity;

    [Header("Road Parameters")]
    public float speedLimit = 5;
    public static float SpeedLimit;
    public float laneOffset = 0.18f;
    public static float LaneOffset;

    [Header("Temporary Parameters")]
    public PathCreator pathCreator;
    public static PathCreator PathCreator;

    public void UpdateParameters()
    {
        CarCapacity = carCapactiy;
        SpeedLimit = speedLimit;
        PathCreator = pathCreator;
        LaneOffset = laneOffset;
        GameManager = gameManager;
    }
}
