using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RoadInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;

    public void Init(GameManager gm)
    {
        int id = 0;
        foreach(GameObject g in RoadGameObjects)
        {
            var rc = g.GetComponent<RoadProperties>();
            Road r = new Road(g, rc.RoadType);
            rc.Id = id;
            gm.Roads.Add(r);
            id++;
        }
    }
}
