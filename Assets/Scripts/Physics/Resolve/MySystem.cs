using System.Collections.Generic;
using System.Linq;

public class MySystem
{
    public Dictionary<string, Ball> balls;
    public Cue cue;
    public Table table;
    public List<Agent> agents;
    public float t;
    public List<Event> events;

    public bool simulated => events.Count > 0;
    public bool continuized => balls.Values.All(ball => ball.historyCts != null && !ball.historyCts.IsEmpty);
    
    public void UpdateHistory(Event _event)
    {
        t = _event.time;
        foreach(var ball in balls.Values)
        {
            ball.state.t = _event.time;
            //ball.history.Add(ball.state);
            ball.history.Add(ball.state);
        }
        events.Append(_event);
    }
    public void ResetHistory(Event _event)
    {
        t = 0;
        foreach(var ball in balls.Values)
        {
            ball.history = new();
            ball.historyCts = new();
            ball.state.t = 0;
        }
        events.Clear();
    }
    public void ResetBalls()
    {
        foreach(var ball in balls.Values)
        {
            if (!ball.history.IsEmpty)
                ball.state = ball.history[0].Copy();
        }
    }

}