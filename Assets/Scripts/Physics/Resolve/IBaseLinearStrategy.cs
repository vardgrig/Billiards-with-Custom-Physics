using System;

public interface IBaseLinearStrategy
{
    Ball MakeKiss(Ball ball, LinearCushionSegment cushion);
    Tuple<Ball, LinearCushionSegment> Resolve(Ball ball, LinearCushionSegment cushion, bool inplace = false);
}
