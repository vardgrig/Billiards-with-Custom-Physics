using UnityEngine;
using System.Collections.Generic;

public class Resolver : MonoBehaviour
{
    IBallBallCollisionStrategy ball_ball;
    IBallPocketStrategy ball_pocket;
    IStickBallCollisionStrategy stick_ball;

}

public enum ArgTypeEnum
{
    FLOAT,INT,STR,BOOL
}
public class ResolveTypes
{
    public Dictionary<string, ArgTypeEnum> ModelArgs = new();
}
public class ResolverConfig
{
    BallBallModel ball_ball;
    Dictionary<string, ArgTypeEnum> ball_ball_params;
    BallCCushionModel ball_ccushion;
    Dictionary<string, ArgTypeEnum> ball_ccushion_params;
    BallLCushionModel ball_lcushion;
    Dictionary<string, ArgTypeEnum> ball_lcushion_params;
    BallPocketModel ball_pocket;
    Dictionary<string, ArgTypeEnum> ball_pocket_params;
    StickBallModel stick_ball;
    Dictionary<string, ArgTypeEnum> stick_ball_params;
    BallTransitionModel transition;
    Dictionary<string, ArgTypeEnum> transition_params;

}