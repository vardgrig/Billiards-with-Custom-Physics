using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Resolver : MonoBehaviour
{
    IBallBallCollisionStrategy ball_ball;
    IBallLCushionCollisionStrategy ball_lcushion;
    IBallCCushionCollisionStrategy ball_ccushion;
    IBallPocketStrategy ball_pocket;
    IStickBallCollisionStrategy stick_ball;
    IBallTransitionStrategy transition;

    void Resolve(MySystem shot, Event _event)
    {
        SnapshotInitial(shot, _event);
        var ids = _event.ids();

        if (_event.eventType == EventTypeEnum.None)
            return;
        else if (_event.eventType.IsTransition())
        {
            Ball ball = shot.balls[ids[0]];
            transition.Resolve(ball, _event.eventType, true);
        }
        else if(_event.eventType == EventTypeEnum.BALL_BALL)
        {
            Ball ball1 = shot.balls[ids[0]];
            Ball ball2 = shot.balls[ids[1]];
            ball_ball.Resolve(ball1, ball2, true);
            ball1.state.t = _event.time;
            ball2.state.t = _event.time;
        }
        else if(_event.eventType == EventTypeEnum.BALL_LINEAR_CUSHION)
        {
            Ball ball = shot.balls[ids[0]];
            LinearCushionSegment cushion = shot.table.cushionSegments.Linear[ids[1]];
            ball_lcushion.Resolve(ball, cushion, true);
            ball.state.t = _event.time;
        }
        else if (_event.eventType == EventTypeEnum.BALL_CIRCULAR_CUSHION)
        {
            Ball ball = shot.balls[ids[0]];
            CircularCushionSegment cushion_jaw = shot.table.cushionSegments.Circular[ids[1]];
            ball_ccushion.Resolve(ball, cushion_jaw, true);
            ball.state.t = _event.time;
        }
        else if(_event.eventType == EventTypeEnum.BALL_POCKET)
        {
            Ball ball = shot.balls[ids[0]];
            Pocket pocket = shot.table.pockets[ids[1]];
            ball_pocket.Resolve(ball, pocket, true);
            ball.state.t = _event.time;
        }
        else if(_event.eventType == EventTypeEnum.STICK_BALL)
        {
            Cue cue = shot.cue;
            Ball ball = shot.balls[ids[0]];
            stick_ball.Resolve(cue, ball, true);
            ball.state.t = _event.time;
        }
        SnapshotFinal(shot, _event);
    }
    void SnapshotInitial(MySystem shot, Event _event)
    {
        foreach(Agent agent in _event.agents)
        {
            if (agent.agentType == AgentTypeEnum.CUE)
                agent.SetInitial(shot.cue as IPoolObject);
            else if (agent.agentType == AgentTypeEnum.BALL)
                agent.SetInitial(shot.balls[agent.id] as IPoolObject);
            else if(agent.agentType == AgentTypeEnum.POCKET)
                agent.SetInitial(shot.table.pockets[agent.id] as IPoolObject);
            else if(agent.agentType == AgentTypeEnum.LINEAR_CUSHION_SEGMENT)
                agent.SetInitial(shot.table.cushionSegments.Linear[agent.id] as IPoolObject);
            else if (agent.agentType == AgentTypeEnum.CIRCULAR_CUSHION_SEGMENT)
                agent.SetInitial(shot.table.cushionSegments.Circular[agent.id] as IPoolObject);
        }
    }
    void SnapshotFinal(MySystem shot, Event _event)
    {
        foreach(Agent agent in shot.agents)
        {
            if(agent.agentType == AgentTypeEnum.BALL)
                agent.SetFinal(shot.balls[agent.id]);
            else if(agent.agentType ==AgentTypeEnum.POCKET)
                agent.SetFinal(shot.table.pockets[agent.id] as IPoolObject);
        }
    }
}
