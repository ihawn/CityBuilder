using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerBase
{
    public static void UpdateControllers()
    {
        UpdateVehicles();
    }

    static void UpdateVehicles()
    {
        foreach (var vehicle in GlobalSettings.GameManager.Vehicles)
        {
            vehicle.VehicleController.FollowPath();
        }
    }
}
