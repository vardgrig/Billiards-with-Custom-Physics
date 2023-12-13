using System;

public interface IBallLCushionCollisionStrategy : IBaseLinearStrategy
{
    Tuple<Ball, LinearCushionSegment> Solve(Ball ball, LinearCushionSegment cushion);
}
