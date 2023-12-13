using System;

public interface IStickBallCollisionStrategy : IBaseStrategy2
{
    Tuple<(Cue, Ball)> Solve(Cue cue, Ball ball);
}
