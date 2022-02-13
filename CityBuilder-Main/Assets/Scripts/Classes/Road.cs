using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Road
{
    public ObjectSettings ObjectSettings { get; set; }
    public float SpeedLimit { get; set; }

    public Road(
        GameObject obj,
        float? speedLimit = null,
        float? width = null
        )
    {
        ObjectSettings = new ObjectSettings(obj);

        if (speedLimit != null)
            SpeedLimit = speedLimit.Value;
        else
            SpeedLimit = GlobalSettings.SpeedLimit;
    }
}
