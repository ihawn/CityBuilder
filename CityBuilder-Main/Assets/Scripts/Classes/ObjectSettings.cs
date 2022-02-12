using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectSettings
{
    public GameObject GameObject { get; set; }
    public Material Material { get; set; }
    public List<GameObject> Children { get; set; }

    public ObjectSettings(GameObject gameObject)
    {
        GameObject = gameObject;
        Material = gameObject.GetComponent<Material>();
        Children = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
            Children.Add(child.gameObject);
    }
}
