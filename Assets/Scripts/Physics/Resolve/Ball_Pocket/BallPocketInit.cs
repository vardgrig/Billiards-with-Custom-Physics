using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBallPocketStrategy
{
    Tuple<Ball, Pocket> Resolve(Ball ball, Pocket pocket, bool inplace = false);
}
public enum BallPocketModel
{
    CANONICAL
}

public class BallPocketInit : IBallPocketStrategy
{
    //Dictionary<int, IBallPocketStrategy> ballPocketModels = new();
    //public BallPocketInit() 
    //{
    //    ballPocketModels[(int)BallPocketModel.CANONICAL] = this;
    //}
    public Tuple<Ball, Pocket> Resolve(Ball ball, Pocket pocket, bool inplace = false)
    {
        if(!inplace)
        {
            ball = ball.Copy();
            pocket = pocket.Copy();
        }

        var rvw = new Vector3[]
        {
            new(pocket.A,-pocket.Depth, pocket.B),
            Vector3.zero,
            Vector3.zero
        };
        ball.state = new BallState(rvw, Constants.BallStates.Pocketed, 0);
        pocket.Add(ball.id);
        return Tuple.Create(ball, pocket); 
    }
    IBallPocketStrategy GetBallPocketModel()
    {
        return this;
    }
}
