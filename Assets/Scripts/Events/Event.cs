using System.Collections.Generic;
using System.Linq;

public class Event
{
    public EventTypeEnum eventType;
    public float time;
    public List<Agent> agents = new();

    public List<string> ids()
    {
        return agents.Select(agent => agent.id).ToList();
    }
    public Event Copy()
    {
        return new Event()
        {
            eventType = eventType,
            time = time,
            agents = agents
        };
    }
}
