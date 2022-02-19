using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionInit : MonoBehaviour
{
    public List<GameObject> IntersectionGameObjects;

    public void Init()
    {
        int id = 0;
        GameManager gm = GlobalSettings.GameManager;
        foreach(GameObject g in IntersectionGameObjects)
        {
            Intersection i = new Intersection(g.tag == "FourWay" ? 4 : 3);
            i.Id = id;
            IntersectionController ic = g.GetComponent<IntersectionController>();
            ic.GetComponent<Renderer>().material.SetColor("_Color", ic.LightColors[i.State]);
            i.IntersectionController = ic;
            ic.Intersection = i;
            gm.Intersections.Add(i);
        }
    }
}
