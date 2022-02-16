using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RoadInit : MonoBehaviour
{
    public List<GameObject> RoadGameObjects;
    public List<GameObject> IntersectionGameObjects;

    public void Init(GameManager gm)
    {
        int id_r = 0;
        foreach(GameObject g in RoadGameObjects)
        {
            var rp = g.GetComponent<RoadProperties>();
            Road r = new Road(g, RoadType.road, id_r, Vector3.zero, Vector3.zero);
            rp.Id = id_r;
            rp.PathCreator = r.PathCreator;
            gm.Roads.Add(r);
            id_r++;
        }

        int id_i = 0;
        foreach(GameObject g in IntersectionGameObjects)
        {
            Transform roads = g.transform.Find("Roads");
            var ip = g.GetComponent<IntersectionProperties>();
            var pos = ip.BezierControlLocus;
            for (int k = 0; k < roads.childCount; k++)
            {
                GameObject g2 = roads.GetChild(k).gameObject;
                var rp = g2.GetComponent<RoadProperties>();
                Road r = new Road(g2, RoadType.intersection, id_r, pos, pos);
                rp.Id = id_r;
                gm.Roads.Add(r);
                id_r++;
            }
     
            Intersection i = new Intersection(g);
            ip.Id = id_i;
            gm.Intersections.Add(i);
            id_i++;
        }
    }
}
