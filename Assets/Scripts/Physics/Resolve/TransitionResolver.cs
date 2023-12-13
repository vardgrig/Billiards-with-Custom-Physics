using System.Collections.Generic;

public static class TransitionResolver
{
    static Dictionary<BallTransitionModel, IBallTransitionStrategy> ballTransitionModels = new()
    {
        {BallTransitionModel.CANONICAL, new CanonicalTransition() }
    };
    public static IBallTransitionStrategy GetTransitionModel(BallTransitionModel? model = null)
    {
        if (!model.HasValue)
            return new CanonicalTransition();
        return ballTransitionModels[model.Value];
    }
}

