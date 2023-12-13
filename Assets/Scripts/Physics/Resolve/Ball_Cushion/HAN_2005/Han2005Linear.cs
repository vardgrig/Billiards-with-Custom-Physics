using System;

public class Han2005Linear : CoreBallLCushionCollision
{
    public override Tuple<Ball, LinearCushionSegment> Solve(Ball ball, LinearCushionSegment cushion) 
    {
        var model = new ModelHan2005();
        return model.Solve(ball, cushion);
    }
}
