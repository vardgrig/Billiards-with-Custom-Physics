using System;

public interface IBallPocketStrategy
{
    Tuple<Ball, Pocket> Resolve(Ball ball, Pocket pocket, bool inplace = false);
}
