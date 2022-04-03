using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
    public object ParentPathFollowController { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(ParentPathFollowController is VehicleController)
        {
            VehicleController vc = ParentPathFollowController as VehicleController;
            vc.OnDetectorTriggerEnter(other.gameObject, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ParentPathFollowController is VehicleController)
        {
            VehicleController vc = ParentPathFollowController as VehicleController;
            vc.OnDetectorTriggerExit(other.gameObject, this);
        } 
    }
}
