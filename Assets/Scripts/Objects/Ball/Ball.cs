using UnityEngine;
using System.Collections.Generic;

public class Ball : MonoBehaviour, IPoolObject
{
    public string id { get; set; }
    public BallState state = new();
    public BallParams params_ = new();
    public BallOrientation initialOrientation = new();

    public List<BallHistory> history = new();
    public List<BallHistory> historyCts = new();
    public BallRender BallRender;

    
    // Property to get the coordinate vector of the ball
    public Vector3 xyz
    {
        get { return state.rvw[0]; }
    }

    // Create a deep copy of the Ball object
    IPoolObject IPoolObject.Copy(bool dropHistory = false) 
    {
        Ball copy = new()
        {
            id = id,
            state = state.Copy(),
            params_ = params_.Copy(),
            initialOrientation = initialOrientation.Copy()
        };

        if (!dropHistory)
        {
            copy.history.AddRange(history);
            copy.historyCts.AddRange(historyCts);
        }

        return copy;
    }
    public Ball Copy(bool dropHistory = false)
    {
        Ball copy = new()
        {
            id = id,
            state = state.Copy(),
            params_ = params_.Copy(),
            initialOrientation = initialOrientation.Copy()
        };

        if (!dropHistory)
        {
            copy.history.AddRange(history);
            copy.historyCts.AddRange(historyCts);
        }

        return copy;
    }
    // Create a Ball object using a flattened parameter set
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

    // Create a dummy Ball object
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
