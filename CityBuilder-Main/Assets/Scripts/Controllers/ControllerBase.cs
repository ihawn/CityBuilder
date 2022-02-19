using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerBase
{
    public static void UpdateControllers()
    {
        UpdateVehicles();
        UpdateIntersections();
    }

    static void UpdateVehicles()
    {
        foreach (var vehicle in GlobalSettings.GameManager.Vehicles)
        {
            vehicle.VehicleController.FollowPath();
        }
    }

    static void UpdateIntersections()
    {
        foreach(var intersection in GlobalSettings.GameManager.Intersections)
        {
            intersection.IntersectionController.UpdateIntersections();
        }
    }
}
