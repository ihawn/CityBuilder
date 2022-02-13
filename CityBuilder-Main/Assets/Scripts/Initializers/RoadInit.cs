using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RoadInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;

    public void Init(GameManager gm)
    {
        for(int i = 0; i < RoadGameObjects.Count; i++)
        {
            GameObject g = RoadGameObjects[i];
            PathCreator p = g.GetComponent<PathCreator>();
            Road r = new Road(g, p);
            gm.Roads.Add(r);
        }
    }
}
