using System;
using UnityEngine;

public abstract class CoreBallCCushionCollision
{
    Ball MakeKiss(Ball ball, CircularCushionSegment cushion)
    {
        var normal = cushion.GetNormal(ball.state.rvw);
        normal = Vector3.Dot(normal, ball.state.rvw[1]) > 0 ? normal : -normal;
        var c = new Vector3(cushion.Center[0], cushion.Center[1], ball.state.rvw[0].y);
        var correction = ball.params_.R + cushion.Radius - MathUtilities.Norm3D(ball.state.rvw[0] - c) - Constants.EPS_Space;
        ball.state.rvw[0] += correction * normal;
        return ball;
    }
    Tuple<Ball, CircularCushionSegment> Resolve(Ball ball, CircularCushionSegment cushion, bool inplace = false)
    {
        if (!inplace)
        {
            ball = ball.Copy();
            cushion = cushion.Copy();
        }
        ball = MakeKiss(ball, cushion);
        return Solve(ball, cushion);
    }
    public abstract Tuple<Ball, CircularCushionSegment> Solve(Ball ball, CircularCushionSegment cushion);
}