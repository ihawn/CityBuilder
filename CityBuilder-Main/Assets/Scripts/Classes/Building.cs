using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    public ObjectSettings ObjectSettings { get; set; }
    public BuildingType BuildingType { get; set; }
    public List<Person> Occupants { get; set; }
}

public enum BuildingType
{
    house = 0,
    apartment = 1,
    office = 2,
    restraunt = 3
}

