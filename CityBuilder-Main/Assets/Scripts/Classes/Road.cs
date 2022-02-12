using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Road
{
    public ObjectSettings ObjectSettings { get; set; }
    public int Lanes { get; set; }
    public int SpeedLimit { get; set; }
    public int Health { get; set; }
    public bool IsInterstate { get; set; }
}
