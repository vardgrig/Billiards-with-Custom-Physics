using System;
using System.Xml.Serialization;
using UnityEngine;

public enum StickBallModel
{
    INSTANTANEOUS_POINT
}
public class StickBallCore
{

}
public interface IBaseStrategy
{
    Tuple<Cue, Ball> Resolve(Cue cue, Ball ball, bool inplace = false);
}
public interface IStickBallCollisionStrategy : IBaseStrategy
{
    Tuple<(Cue, Ball)> Solve(Cue cue, Ball ball);
}
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
public class StickBallInit : MonoBehaviour
{

}
