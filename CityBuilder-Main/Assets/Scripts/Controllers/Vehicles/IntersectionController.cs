using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour
{
    public Intersection Intersection;
    public IntersectionState State;
    public float cycleTimer;
    public float nextTime;
    float slowZoneOffset = -2;
    List<IDictionary<string, Light>> lights = new List<IDictionary<string, Light>>();

    public IDictionary<IntersectionState, Color> LightColors = new Dictionary<IntersectionState, Color>()
    {
        { IntersectionState.straight, Color.green },
        { IntersectionState.right, Color.blue },
        { IntersectionState.orthogonal, Color.red },
        { IntersectionState.left, Color.cyan }
    };

    public void Init()
    {
        foreach(Transform trans in transform)
        {
            if(trans.name.Contains("traffic_light"))
            {
                foreach (Transform t in trans)
                {
                    if (t.tag == "light")
                    {
                        var lightDict = new Dictionary<string, Light>();

                        var l1 = t.GetChild(0).GetComponent<Light>();
                        var l2 = t.GetChild(1).GetComponent<Light>();
                        var l3 = t.GetChild(2).GetComponent<Light>();

                        l1.intensity = 0;
                        l2.intensity = 0;
                        l3.intensity = 0;

                        lightDict.Add("red", l1);
                        lightDict.Add("yellow", l2);
                        lightDict.Add("green", l3);

                        lights.Add(lightDict);
                    }
                }
            }
        }
    }

    public void UpdateIntersection()
    {
        Intersection.CycleTimer += Time.deltaTime;
        State = Intersection.State;
        cycleTimer = Intersection.CycleTimer;
        nextTime = Intersection.NextTime;

        //State change
        if(Intersection.CycleTimer >= Intersection.NextTime)
        {
            int stateOffset = 1;
            if (Intersection.State == IntersectionState.left)
                stateOffset = -3;
            else if (Intersection.State == IntersectionState.right && Intersection.PathCount == 3)
                stateOffset = 2;
            Intersection.State += stateOffset;
            Intersection.NextTime = Intersection.Timings[Intersection.State];
            Intersection.CycleTimer = 0;

            SetIntersectionState(Intersection.State);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", LightColors[Intersection.State]);
        }
        else if(!Intersection.OneIsYellow && Intersection.CycleTimer >= Intersection.NextTime - GlobalSettings.YellowLightDuration)
        {
            SetIntersectionState(Intersection.State, oneIsYellow: true);
        }
    }

    public void SetIntersectionState(IntersectionState state, bool oneIsYellow = false)
    {
        switch (state)
        {
            case IntersectionState.straight:
                SetIntersectionChildState(false);
                break;

            case IntersectionState.left:
                SetIntersectionChildState(false);
                break;

            case IntersectionState.right:
                SetIntersectionChildState(true);
                break;

            case IntersectionState.orthogonal:
                SetIntersectionChildState(true);
                break;
        }

        if(oneIsYellow)
        {
            if(state == IntersectionState.straight || state == IntersectionState.left)
            {
                SetTrafficLightsAll(true, oneIsYellow);
            }
            else if(state == IntersectionState.right || state == IntersectionState.orthogonal)
            {
                SetTrafficLightsAll(true, oneIsYellow);
            }
        }    
    }

    void SetIntersectionChildState(bool b)
    {
        float activeHeight = slowZoneOffset;
        float inactiveHeight = 10;

        int i = 0;
        foreach(Transform t in transform)
        {
            if(t.name.ToLower().Contains("triggerzone"))
            {
                Vector3 pos;
                if (i == 0 || i == 1) { pos = new Vector3(t.localPosition.x, b ? activeHeight : inactiveHeight, t.localPosition.z); }
                else { pos = new Vector3(t.localPosition.x, !b ? activeHeight : inactiveHeight, t.localPosition.z); }
                t.localPosition = pos;
                i++;
            }
        }

        SetTrafficLightsAll(b, false);
    }

    void SetTrafficLightsAll(bool green, bool oneIsYellow)
    {
        int offset = green ? 1 : 0;
        for(int i = 0; i < 4; i++)
        {
            SetTrafficLightsIndividual(i, (i + offset) % 2 == 0, oneIsYellow);
        }
    }

    void SetTrafficLightsIndividual(int index, bool green, bool oneIsYellow)
    {
        float lux = GlobalSettings.TrafficLightIntensity;
        bool yellow = (oneIsYellow && index % 2 == 0) || (oneIsYellow && index % 2 == 1);

        lights[index]["red"].intensity = green ? 0 : lux;
        lights[index]["green"].intensity = green && !oneIsYellow ? lux*0.5f : 0;
        lights[index]["yellow"].intensity = green && oneIsYellow ? lux*0.75f : 0;

        lights[index]["red"].gameObject.SetActive(!green);
        lights[index]["green"].gameObject.SetActive(green && !yellow);
        lights[index]["yellow"].gameObject.SetActive(green && yellow);

        Intersection.OneIsYellow = oneIsYellow;
    }
}