using System;
using UnityEngine;

public class CanonicalBallPocket : IBallPocketStrategy
{
    public Tuple<Ball, Pocket> Resolve(Ball ball, Pocket pocket, bool inplace = false)
    {
        if(!inplace)
        {
            ball = ball.Copy();
            pocket = pocket.Copy();
        }
        var rvw = new Vector3[3]{
            new Vector3(pocket.A, -pocket.Depth, pocket.B),
            Vector3.zero,
            Vector3.zero,
        };
        ball.state = new BallState(rvw, Constants.BallStates.Pocketed, 0);
        pocket.Add(ball.id);
        return Tuple.Create(ball, pocket);
    }
}