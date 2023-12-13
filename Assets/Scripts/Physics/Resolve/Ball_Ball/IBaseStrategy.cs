using System;

public interface IBaseStrategy
{
    Tuple<Ball, Ball> MakeKiss(Ball ball1, Ball ball2);
    Tuple<Ball, Ball> Resolve(Ball ball1, Ball ball2, bool inplace = false);
}
