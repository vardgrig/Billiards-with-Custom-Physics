using System;
using UnityEngine;

public abstract class CoreBallLCushionCollision
{
    Ball MakeKiss(Ball ball, LinearCushionSegment cushion)
    {
        var normal = cushion.GetNormal(ball.state.rvw);
        normal = Vector3.Dot(normal, ball.state.rvw[1]) > 0 ? normal : -normal;
        var c = MathUtilities.PointOnLineClosestToPoint(cushion.P1, cushion.P2, ball.state.rvw[0]);
        c[2] = ball.state.rvw[0].y;
        var correction = ball.params_.R - MathUtilities.Norm3D(ball.state.rvw[0] - c) + Constants.EPS_Space;
        ball.state.rvw[0] -= correction * normal;
        return ball;
    }
    Tuple<Ball, LinearCushionSegment> Resolve(Ball ball, LinearCushionSegment cushion, bool inplace = false)
    {
        if (!inplace)
        {
            ball = ball.Copy();
            cushion = cushion.Copy();
        }
        ball = MakeKiss(ball, cushion);
        return Solve(ball, cushion);
    }
    public abstract Tuple<Ball, LinearCushionSegment> Solve(Ball ball, LinearCushionSegment cushion);
}
