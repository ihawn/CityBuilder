using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[System.Serializable]
public class Vehicle
{
    public ObjectSettings ObjectSettings { get; set; }
    public VehicleType VehicleType { get; set; }
    public PathFollow PathFollow { get; set; }
    public List<Cargo> CargoList { get; set; }
    public Person Driver { get; set; }
    public List<Person> Passengers { get; set; }
    public VehicleController VehicleController { get; set; }
    public int Capacity { get; set; }
    public GameObject Obstacle { get; set; }

    public Vehicle(
        VehicleType type,
        GameObject obj,
        PathFollow pathFollow,
        int? capacity = null,
        List<Cargo> cargoList = null,
        Person driver = null,
        List<Person> passengers = null)
    {
        Driver = driver;
        ObjectSettings = new ObjectSettings(obj);
        VehicleController = obj.GetComponent<VehicleController>();
        PathFollow = pathFollow;


        if (cargoList != null)
            CargoList = cargoList;
        else
            CargoList = new List<Cargo>();

        if(passengers != null)
            Passengers = passengers;
        else
            Passengers = new List<Person>();

        switch (type)
        {
            case VehicleType.car:
                VehicleType = VehicleType.car;
                Capacity = capacity == null ? GlobalSettings.CarCapacity : capacity.Value;
                break;
        }
    }
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
