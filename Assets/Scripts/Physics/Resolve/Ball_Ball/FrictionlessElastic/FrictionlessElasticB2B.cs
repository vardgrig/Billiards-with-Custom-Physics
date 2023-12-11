using System;
using UnityEngine;

public class FrictionlessElastic : BallBallCore
{
    public override Tuple<Ball, Ball> Solve(Ball ball1, Ball ball2)
    {
        var res = ResolveBallBallCollision(ball1.state.rvw, ball2.state.rvw, ball1.params_.R);
        ball1.state = new BallState(res.Item1, Constants.BallStates.Sliding, 0);
        ball2.state = new BallState(res.Item2, Constants.BallStates.Sliding, 0);

        return Tuple.Create(ball1, ball2);
    }

    private Tuple<Vector3[], Vector3[]> ResolveBallBallCollision(Vector3[] rvw1, Vector3[] rvw2, float R)
    {
        Vector3 r1 = rvw1[0];
        Vector3 v1 = rvw1[1];

        Vector3 r2 = rvw2[0];
        Vector3 v2 = rvw2[1];

        Vector3 n = MathUtilities.UnitVector(r2 - r1);
        Vector3 t = MathUtilities.CoordinateRotation(n, Mathf.PI / 2);

        Vector3 v_rel = v1 - v2;

        float v_mag = MathUtilities.Norm3D(v_rel);

        float beta = MathUtilities.Angle(v_rel, n);

        rvw1[1] = Mathf.Sin(beta) * v_mag * t + v2;
        rvw2[1] = Mathf.Cos(beta) * v_mag * n + v2;

        return Tuple.Create(rvw1, rvw2);
    }
}
