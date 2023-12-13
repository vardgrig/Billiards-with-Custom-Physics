using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour, IPoolObject
{
    public string id { get; set; }
    public BallState state = new();
    public BallParams params_ = new();
    public BallOrientation initialOrientation = new();

    public BallHistory history = new();
    public BallHistory historyCts = new();
    public BallRender BallRender;

    
    public Vector3 xyz
    {
        get { return state.rvw[0]; }
    }

    public Ball()
    {

    }
    public Ball(Ball other,bool dropHistory = false)
    {
        id = other.id;
        state = other.state.Copy();
        history = !dropHistory ? other.history.Copy() : new BallHistory();
        historyCts = !dropHistory ? other.historyCts.Copy() : new BallHistory();
    }
    IPoolObject IPoolObject.Copy(bool dropHistory = false) 
    {
        return new Ball(this, dropHistory);
    }
    public Ball Copy(bool dropHistory = false)
    {
        return new Ball(this, dropHistory);
    }
    public static Ball Create(string id, Vector2 xy, Dictionary<string, object> kwargs)
    {
        Ball ball = new()
        {
            id = id,
            //params_ = new BallParams(kwargs)
            params_ = new BallParams()
        };

        if (xy != null)
        {
            ball.state.rvw[0] = new Vector3(xy.x, ball.params_.R, xy.y);
        }

        return ball;
    }

    public static Ball Dummy(string id = "dummy")
    {
        Ball ball = new()
        {
            id = id
        };
        return ball;
    }
    private void FixedUpdate()
    {
        transform.position = state.rvw[0];
    }
}
