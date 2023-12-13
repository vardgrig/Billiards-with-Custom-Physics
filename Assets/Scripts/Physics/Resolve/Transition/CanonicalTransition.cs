using System;
using UnityEngine;

public class CanonicalTransition
{
    Ball Resolve(Ball ball, EventTypeEnum transition, bool inplace = false)
    {
        if (!inplace)
            ball = ball.Copy();
        Tuple<int, int> start_end = BallTransitionMotionStates(transition);
        var start = start_end.Item1;
        var end = start_end.Item2;

        if (end == (int)Constants.BallStates.Spinning)
        {
            var v = ball.state.rvw[1];
            var w = ball.state.rvw[2];

            ball.state.rvw[1] = Vector3.zero;
            ball.state.rvw[2] = new Vector3(0, ball.state.rvw[2].y, 0);
        }
        else if(end == (int)Constants.BallStates.Stationary)
        {
            var v = ball.state.rvw[1];
            var w = ball.state.rvw[2];

            ball.state.rvw[1] = Vector3.zero;
            ball.state.rvw[2] = Vector3.zero;
        }

        return ball; ;
    }

    Tuple<int, int> BallTransitionMotionStates(EventTypeEnum eventType)
    {
        if (eventType == EventTypeEnum.SPINNING_STATIONARY)
            return Tuple.Create((int)Constants.BallStates.Spinning, (int)Constants.BallStates.Stationary);
        else if (eventType == EventTypeEnum.ROLLING_STATIONARY)
            return Tuple.Create((int)Constants.BallStates.Rolling, (int)Constants.BallStates.Stationary);
        else if (eventType == EventTypeEnum.ROLLING_SPINNIG)
            return Tuple.Create((int)Constants.BallStates.Rolling, (int)Constants.BallStates.Spinning);
        else if (eventType == EventTypeEnum.SLIDING_ROLLING)
            return Tuple.Create((int)Constants.BallStates.Sliding, (int)Constants.BallStates.Rolling);

        throw new NotImplementedException();
    }
}
