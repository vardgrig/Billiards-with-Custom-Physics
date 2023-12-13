using System.Collections.Generic;

public static class BallBallCollisionResolver
{
    public static Dictionary<BallBallModel, IBallBallCollisionStrategy> ballBallModels = new()
    {
        {BallBallModel.FRICTIONLESS_ELASTIC, new FrictionlessElastic()}
    };

    public static IBallBallCollisionStrategy GetBallBallModel(BallBallModel? model = null)
    {
        if(!model.HasValue)
            return new FrictionlessElastic();

        return ballBallModels[model.Value];
    }
}
