using UnityEngine;

public class BallState
{
    public Vector3[] rvw = new Vector3[3];
    public float t = 0.0f;                
    public Constants.BallStates state;

    public BallState(Vector3[] rvw, Constants.BallStates s, float t)
    {
        this.rvw = rvw;
        this.state = s;
        this.t = t;
    }

    public bool Equals(BallState other)
    {
        if(this.state == other.state && this.t == other.t && this.rvw == other.rvw)
            return true;
        return false;
    }

    public BallState Copy()
    {
        // Create a deep copy
        Vector3[] copiedRvw = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            copiedRvw[i] = rvw[i];
        }

        return new BallState(copiedRvw, state, t);
    }

    public BallState()
    {
        this.rvw = new Vector3[3];
        this.state = Constants.BallStates.Stationary;
        this.t = 0;

        Debug.Log("Default Constructor for BallState");
    }
}
