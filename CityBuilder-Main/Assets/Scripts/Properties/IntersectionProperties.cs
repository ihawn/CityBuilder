using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionProperties : MonoBehaviour
{
    public int Id;
    public Vector3 BezierControlLocus;

    private void Awake()
    {
        BezierControlLocus = transform.GetChild(0).position;
    }
}
