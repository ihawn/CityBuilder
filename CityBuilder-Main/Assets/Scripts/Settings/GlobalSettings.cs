using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class GlobalSettings : MonoBehaviour
{
    [Header("Global Engine Parameters")]
    public float timescale = 1;

    [Header("Object References")]
    public GameManager gameManager;
    public static GameManager GameManager;

    [Header("Car Parameters")]
    public int carCapactiy = 4;
    public static int CarCapacity;
    public float slowAccelerationVehicle = -3f;
    public static float SlowAccelerationVehicle;
    public float speedAccelerationVehicle = 2f;
    public static float SpeedAccelerationVehicle;
    public float slowdownTrailDropOffset = 0.025f;
    public static float SlowdownTrailDropOffset;
    public float startSlowdownThreshold = 1.5f;
    public static float StartSlowdownThreshold;

    [Header("Road Parameters")]
    public float speedLimit = 5;
    public static float SpeedLimit;
    public float laneOffset = 0.18f;
    public static float LaneOffset;
    public Dictionary<IntersectionState, float> intersectionTimings = new Dictionary<IntersectionState, float>()
    {
        { IntersectionState.straight, 10 },
        { IntersectionState.orthogonal, 10 },
        { IntersectionState.left, 5 },
        { IntersectionState.right, 5 }
    };
    public static Dictionary<IntersectionState, float> IntersectionTimings;

    public void UpdateParameters()
    {
        CarCapacity = carCapactiy;
        SpeedLimit = speedLimit;
        LaneOffset = laneOffset;
        GameManager = gameManager;
        IntersectionTimings = intersectionTimings;
        SlowAccelerationVehicle = slowAccelerationVehicle;
        SpeedAccelerationVehicle = speedAccelerationVehicle;
        SlowdownTrailDropOffset = slowdownTrailDropOffset;
        StartSlowdownThreshold = startSlowdownThreshold;
        Time.timeScale = timescale;
    }
}
