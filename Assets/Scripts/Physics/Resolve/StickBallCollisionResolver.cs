using System.Collections.Generic;

public static class StickBallCollisionResolver
{
    static Dictionary<StickBallModel, IStickBallCollisionStrategy> stickBallModels = new()
    {
        {StickBallModel.INSTANTANEOUS_POINT, new InstantaneousPoint() }
    };
    public static IStickBallCollisionStrategy GetStickBallModel(StickBallModel? model = null)
    {
        if(!model.HasValue)
            return new InstantaneousPoint();
        return stickBallModels[model.Value];
    }
}

