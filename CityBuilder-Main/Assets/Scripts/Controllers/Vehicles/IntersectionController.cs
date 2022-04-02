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
        { IntersectionState.orthogonal, Color.red }
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
            Intersection.OneIsYellow = false;

            Intersection.State = Intersection.State == IntersectionState.orthogonal ? IntersectionState.straight : IntersectionState.orthogonal;
            Intersection.NextTime = Intersection.Timings[Intersection.State];
            Intersection.CycleTimer = 0;

            SetIntersectionState(Intersection.State);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", LightColors[Intersection.State]);
        }
        else if(!Intersection.OneIsYellow && Intersection.CycleTimer >= Intersection.NextTime - GlobalSettings.YellowLightDuration)
        {
            SetYellowLight();
        }
    }

    public void SetIntersectionState(IntersectionState state)
    {
        switch (state)
        {
            case IntersectionState.straight:
                SetIntersectionChildState(false);
                break;

            case IntersectionState.orthogonal:
                SetIntersectionChildState(true);
                break;
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
            SetTrafficLightsIndividual(i, (i + offset) % 2 == 0);
        }
    }

    void SetTrafficLightsIndividual(int index, bool green)
    {
        float lux = GlobalSettings.TrafficLightIntensity;

        lights[index]["red"].intensity = green ? 0 : lux;
        lights[index]["green"].intensity = green ? lux * 0.5f : 0;
        lights[index]["yellow"].intensity = 0;

        lights[index]["red"].gameObject.SetActive(!green);
        lights[index]["green"].gameObject.SetActive(green);
        lights[index]["yellow"].gameObject.SetActive(false);
    }

    void SetYellowLight()
    {
        float lux = GlobalSettings.TrafficLightIntensity;

        //determine which lights are going from green to red (hence yellow)
        bool whichYellow = Intersection.State == IntersectionState.orthogonal;
        int yellow1 = whichYellow ? 1 : 0;
        int yellow2 = whichYellow ? 2 : 3;

        lights[yellow1]["yellow"].gameObject.SetActive(true);
        lights[yellow2]["yellow"].gameObject.SetActive(true);
        lights[yellow1]["yellow"].intensity = lux * 0.75f;
        lights[yellow2]["yellow"].intensity = lux * 0.75f;

        lights[yellow1]["green"].gameObject.SetActive(false);
        lights[yellow2]["green"].gameObject.SetActive(false);
        lights[yellow1]["green"].intensity = 0;
        lights[yellow2]["green"].intensity = 0;

        Intersection.OneIsYellow = true;
    }
}