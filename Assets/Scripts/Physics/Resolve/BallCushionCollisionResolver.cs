using System.Collections.Generic;

public static class BallCushionCollisionResolver
{
    static Dictionary<BallLCushionModel, IBallLCushionCollisionStrategy> ballLCushionModels = new()
    {
        { BallLCushionModel.HAN_2005, new Han2005Linear() }
    };
    static Dictionary<BallCCushionModel, IBallCCushionCollisionStrategy> ballCCushionModels = new()
    {
        { BallCCushionModel.HAN_2005, new Han2005Circular() }
    };
    public static IBallLCushionCollisionStrategy GetBallLinCushionModel(BallLCushionModel? model = null)
    {
        if (!model.HasValue)
            return new Han2005Linear();

        return ballLCushionModels[model.Value];
    }
    public static IBallCCushionCollisionStrategy GetBallCircCushionModel(BallCCushionModel? model = null)
    {
        if (!model.HasValue)
            return new Han2005Circular();

        return ballCCushionModels[model.Value];
    }
}
