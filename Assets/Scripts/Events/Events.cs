using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event : MonoBehaviour
{
    public EventTypeEnum eventType;
    public float time;
    public List<Agent> agents = new();

    public List<string> ids()
    {
        return agents.Select(agent => agent.id).ToList();
    }
}
public class EventDatatypes
{

}
