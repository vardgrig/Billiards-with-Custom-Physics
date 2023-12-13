using System.Collections.Generic;

public class ResolverConfig
{
    public BallBallModel ball_ball;
    public Dictionary<string, ArgTypeEnum> ball_ball_params;
    public BallCCushionModel ball_ccushion;
    public Dictionary<string, ArgTypeEnum> ball_ccushion_params;
    public BallLCushionModel ball_lcushion;
    public Dictionary<string, ArgTypeEnum> ball_lcushion_params;
    public BallPocketModel ball_pocket;
    public Dictionary<string, ArgTypeEnum> ball_pocket_params;
    public StickBallModel stick_ball;
    public Dictionary<string, ArgTypeEnum> stick_ball_params;
    public BallTransitionModel transition;
    public Dictionary<string, ArgTypeEnum> transition_params;

    public ResolverConfig Default()
    {
        var config = new ResolverConfig()
        {
            ball_ball = BallBallModel.FRICTIONLESS_ELASTIC,
            ball_ball_params = new(),
            ball_lcushion = BallLCushionModel.HAN_2005,
            ball_lcushion_params = new(),
            ball_ccushion = BallCCushionModel.HAN_2005,
            ball_ccushion_params = new(),
            stick_ball = StickBallModel.INSTANTANEOUS_POINT,
            stick_ball_params = new(),
            transition = BallTransitionModel.CANONICAL,
            transition_params = new()
        };
        return config;
    }
}

