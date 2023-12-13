using System;

public interface IBaseCircularStrategy
{
    Ball MakeKiss(Ball ball, CircularCushionSegment cushion);
    Tuple<Ball, CircularCushionSegment> Resolve(Ball ball, CircularCushionSegment cushion, bool inplace = false);
}
