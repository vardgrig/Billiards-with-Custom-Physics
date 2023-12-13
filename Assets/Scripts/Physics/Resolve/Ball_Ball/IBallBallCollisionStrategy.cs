using System;

public interface IBallBallCollisionStrategy : IBaseStrategy
{
    Tuple<Ball, Ball> Solve(Ball ball1, Ball ball2);
}
