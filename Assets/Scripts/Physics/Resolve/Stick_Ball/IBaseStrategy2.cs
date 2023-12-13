using System;

public interface IBaseStrategy2
{
    Tuple<Cue, Ball> Resolve(Cue cue, Ball ball, bool inplace = false);
}
