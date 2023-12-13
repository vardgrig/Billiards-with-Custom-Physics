using System;

public abstract class CoreBallBallCollision : IBallBallCollisionStrategy
{
    public abstract Tuple<Ball, Ball> Solve(Ball ball1, Ball ball2);

    public Tuple<Ball, Ball> MakeKiss(Ball ball1, Ball ball2)
    {
        var r1 = ball1.state.rvw[0];
        var r2 = ball2.state.rvw[0];
        var n = (r2 - r1).normalized;

        var correction = 2 * ball1.params_.R - (r2 - r1).magnitude + Constants.EPS_Space;
        ball2.state.rvw[0] += correction / 2 * n;
        ball1.state.rvw[0] -= correction / 2 * n;

        return Tuple.Create(ball1, ball2);
    }
    public Tuple<Ball, Ball> Resolve(Ball ball1, Ball ball2, bool inplace = false)
    {
        if (!inplace)
        {
            ball1 = ball1.Copy();
            ball2 = ball2.Copy();
        }

        var res = MakeKiss(ball1, ball2);
        ball1 = res.Item1;
        ball2 = res.Item2;

        return Solve(ball1, ball2);
    }
}