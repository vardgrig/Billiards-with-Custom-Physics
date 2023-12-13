public interface IBallTransitionStrategy
{
    Ball Resolve(Ball ball, EventTypeEnum transition, bool inplace = false);
}

