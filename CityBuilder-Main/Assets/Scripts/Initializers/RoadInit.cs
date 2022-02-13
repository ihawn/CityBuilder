using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RoadInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;

    public void Init(GameManager gm)
    {
        foreach(GameObject g in RoadGameObjects)
        {
            Road r = new Road(g);
            gm.Roads.Add(r);
        }
    }
}
