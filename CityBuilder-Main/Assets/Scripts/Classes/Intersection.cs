using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    public List<Road> Roads { get; set; }
    public GameObject IntersectionGameObject { get; set; }

    public Intersection(GameObject obj)
    {
        IntersectionGameObject = obj;
        Roads = new List<Road>();
        GameObject roadParent = obj.transform.Find("Roads").gameObject;
        foreach(Transform child in roadParent.transform)
        {
            int id = child.GetComponent<RoadProperties>().Id;
            Roads.Add(GlobalSettings.GameManager.Roads[id]);
        }
    }
}
