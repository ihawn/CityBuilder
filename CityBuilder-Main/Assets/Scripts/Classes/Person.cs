using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public ObjectSettings ObjectSettings { get; set; }
    public PersonType PersonType { get; set; }
    public int Age { get; set; }
    public int Worth { get; set; }
    public int Exhaustion { get; set; }
    public int Happiness { get; set; }
}

public enum PersonType
{
    worker = 0,
    police = 1,
    unproductive = 2,
    child = 3,
    elder = 4
}