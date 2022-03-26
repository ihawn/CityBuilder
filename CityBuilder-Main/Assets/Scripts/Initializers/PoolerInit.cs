using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerInit : MonoBehaviour
{
    public PoolingAgent[] Poolers;
    public void Init()
    {
        foreach(PoolingAgent p in Poolers)
        {
            p.Init();
        }
    }
}
