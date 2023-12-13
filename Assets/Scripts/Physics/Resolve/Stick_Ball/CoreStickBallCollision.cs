using System;

public abstract class CoreStickBallCollision
{
    public virtual Tuple<Cue, Ball> Resolve(Cue cue, Ball ball, bool inplace = false)
    {
        if (!inplace)
        {
            cue = cue.Copy();
            ball = ball.Copy();
        }
        return this.Solve(cue, ball);
    }
    public abstract Tuple<Cue, Ball> Solve(Cue cue, Ball ball);
}
