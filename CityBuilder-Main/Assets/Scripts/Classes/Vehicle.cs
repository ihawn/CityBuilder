using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle
{
    public ObjectSettings ObjectSettings { get; set; }
    public VehicleType VehicleType { get; set; }
    public List<Cargo> CargoList { get; set; }
    public List<Person> Occupants { get; set; }
    public int Capaciy { get; set; }
    public float Speed { get; set; }
}

public enum VehicleType
{
    car = 0,
    truck = 1,
    semi = 2,
    tractor = 3,
    train = 4,
    boat = 5,
    plane = 6
}
