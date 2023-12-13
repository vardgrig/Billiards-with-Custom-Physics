using System;

public class Han2005Circular : CoreBallCCushionCollision
{
    public override Tuple<Ball, CircularCushionSegment> Solve(Ball ball, CircularCushionSegment cushion)
    {
        var model = new ModelHan2005();
        return model.Solve(ball, cushion);
    }
}
