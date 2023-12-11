using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Event : MonoBehaviour
{
    public EventTypeEnum eventType;

}
#region Event Types
public enum EventTypeEnum
{
    None,
    BALL_BALL,
    BALL_LINEAR_CUSHION,
    BALL_CIRCULAR_CUSHION,
    BALL_POCKET,
    STICK_BALL,
    SPINNING_STATIONARY,
    ROLLING_STATIONARY,
    ROLLING_SPINNIG,
    SLIDING_ROLLING
}
public static class EventType
{
    public  static bool IsCollision(this EventTypeEnum eventType)
    {
        return new List<EventTypeEnum>
        {
            EventTypeEnum.BALL_BALL,
            EventTypeEnum.BALL_CIRCULAR_CUSHION,
            EventTypeEnum.BALL_LINEAR_CUSHION,
            EventTypeEnum.BALL_POCKET,
            EventTypeEnum.STICK_BALL
        }.Contains(eventType);
    }
    public static bool IsTransition(this EventTypeEnum eventType)
    {
        return new List<EventTypeEnum>
        {
            EventTypeEnum.SPINNING_STATIONARY,
            EventTypeEnum.ROLLING_STATIONARY,
            EventTypeEnum.ROLLING_SPINNIG,
            EventTypeEnum.SLIDING_ROLLING
        }.Contains(eventType);
    }
}
#endregion
#region AgentType
public enum AgentTypeEnum
{
    NULL,
    CUE,
    BALL,
    POCKET,
    LINEAR_CUSHION_SEGMENT,
    CIRCULAR_CUSHION_SEGMENT
}
public static class AgentType
{
    public static Dictionary<Type, AgentTypeEnum> ClassToType = new Dictionary<Type, AgentTypeEnum>
    {
        //{ typeof(NullObject), AgentType.NULL },
        { typeof(Cue), AgentTypeEnum.CUE },
        { typeof(Ball), AgentTypeEnum.BALL },
        { typeof(Pocket), AgentTypeEnum.POCKET },
        { typeof(LinearCushionSegment), AgentTypeEnum.LINEAR_CUSHION_SEGMENT },
        { typeof(CircularCushionSegment), AgentTypeEnum.CIRCULAR_CUSHION_SEGMENT }
    };
    public static Dictionary<AgentTypeEnum, Type> TypeToClass = new Dictionary<AgentTypeEnum, Type>
    {
        //{ AgentTypeEnum.NULL, typeof(NullObject) },
        { AgentTypeEnum.CUE, typeof(Cue) },
        { AgentTypeEnum.BALL, typeof(Ball) },
        { AgentTypeEnum.POCKET, typeof(Pocket) },
        { AgentTypeEnum.LINEAR_CUSHION_SEGMENT, typeof(LinearCushionSegment) },
        { AgentTypeEnum.CIRCULAR_CUSHION_SEGMENT, typeof(CircularCushionSegment) }
    };
}
#endregion

public interface IAgent<T> where T : IPoolObject
{
    string id { get; set; }
    AgentTypeEnum agentType { get; set; }
    T initial { get; set; }
    T final { get; set; }

    void SetInitial(T obj);
}
public class Agent : IAgent<IPoolObject>
{
    public string id { get; set; }
    public AgentTypeEnum agentType { get; set; }
    public IPoolObject initial { get; set; }
    public IPoolObject final { get; set; }

    public void SetInitial(IPoolObject _object)
    {
        if (agentType == AgentTypeEnum.NULL)
            return;

        if(agentType == AgentTypeEnum.BALL)
        {
            initial = _object.Copy(false);
        }
        else
        {
            initial = _object.Copy();
        }
    }   
    public void SetFinal(IPoolObject _object)
    {
        if (agentType == AgentTypeEnum.NULL)
            return;

        if(agentType == AgentTypeEnum.BALL)
        {
            final = _object.Copy(false);
        }
        else
        {
            final = _object.Copy();
        }
    }
    public bool Matches(IPoolObject _object)
    {
        var correct_class = AgentType.ClassToType[_object.GetType()] == agentType;
        return correct_class && _object.id == id;
    }
    public static Agent FromObject(IPoolObject _object, bool setInitial = false)
    {
        var agent = new Agent()
        {
            id = _object.id,
            agentType = AgentType.ClassToType[_object.GetType()]
        };
        if (setInitial)
            agent.SetInitial(_object);
        return agent;
    }
    public Agent Copy()
    {
        return null; // InitEvolve(this);
    }

}
public class EventDatatypes
{

}
