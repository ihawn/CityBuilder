using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoadPiece
{
    public ObjectSettings ObjectSettings { get; set; }
    public float SpeedLimit { get; set; }

    public RoadPiece(
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
