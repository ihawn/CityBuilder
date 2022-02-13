using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class GlobalSettings : MonoBehaviour
{
    [Header("Car Parameters")]
    public int carCapactiy = 4;
    public static int CarCapacity;

    [Header("Road Parameters")]
    public float residentialRoadSpeed = 5;
    public static float ResidentialRoadSpeed;

    [Header("Temporary Parameters")]
    public PathCreator pathCreator;
    public static PathCreator PathCreator;

    void Awake()
    {
        CarCapacity = carCapactiy;
        ResidentialRoadSpeed = residentialRoadSpeed;
        PathCreator = pathCreator;
    }
}
