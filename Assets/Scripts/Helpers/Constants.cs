public static class Constants
{
    public enum BallStates
    {
        Stationary, Spinning, Sliding, Rolling, Pocketed
    };

    public static float EPS = float.Epsilon * 100;
    public static float EPS_Space = 1e-9f;

    public static float english_fraction = 0.5000001f;
}