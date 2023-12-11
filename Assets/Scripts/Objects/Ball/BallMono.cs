using UnityEngine;

public class BallMono : MonoBehaviour
{
    public Ball ball;

    private void Awake()
    {
        ball = new();
    }
}
