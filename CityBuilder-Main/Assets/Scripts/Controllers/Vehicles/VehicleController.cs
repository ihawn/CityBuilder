using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Vehicle vehicle;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PathFork")
        {
            var ic = other.GetComponent<Intersection>();
            int numTurns = ic.Roads.Count;
            int choice = Random.Range(0, numTurns);
            Road newFollow = ic.Roads[choice];
            vehicle.PathFollow.RoadFollowed = GlobalSettings.GameManager.Roads[newFollow.Id];
        }
    }
}
