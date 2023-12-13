using System;

public interface IBallCCushionCollisionStrategy : IBaseCircularStrategy
{
    Tuple<Ball, CircularCushionSegment> Solve(Ball ball, CircularCushionSegment cushion);
}
