using System.Collections.Generic;

public static class EventType
{
    public  static bool IsCollision(this EventTypeEnum eventType)
    {
        return new List<EventTypeEnum>
        {
            EventTypeEnum.BALL_BALL,
            EventTypeEnum.BALL_CIRCULAR_CUSHION,
            EventTypeEnum.BALL_LINEAR_CUSHION,
            EventTypeEnum.BALL_POCKET,
            EventTypeEnum.STICK_BALL
        }.Contains(eventType);
    }
    public static bool IsTransition(this EventTypeEnum eventType)
    {
        return new List<EventTypeEnum>
        {
            EventTypeEnum.SPINNING_STATIONARY,
            EventTypeEnum.ROLLING_STATIONARY,
            EventTypeEnum.ROLLING_SPINNIG,
            EventTypeEnum.SLIDING_ROLLING
        }.Contains(eventType);
    }
}