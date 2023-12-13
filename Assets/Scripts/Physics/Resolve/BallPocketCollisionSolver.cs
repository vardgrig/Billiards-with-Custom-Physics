using System.Collections.Generic;

public static class BallPocketCollisionSolver
{
    static Dictionary<BallPocketModel, IBallPocketStrategy> ballPocketModels = new()
    {
        {BallPocketModel.CANONICAL, new CanonicalBallPocket() }
    };
    public static IBallPocketStrategy GetBallPocketModel(BallPocketModel? model = null)
    {
        if (!model.HasValue)
            return new CanonicalBallPocket();

        return ballPocketModels[model.Value];
    }
}