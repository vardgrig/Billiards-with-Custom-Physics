using System;
using System.Collections.Generic;
using UnityEngine;

public class BallHistory
{
    private List<BallState> states = new();

    public BallState this[int idx]
    {
        get { return states[idx]; }
    }

    public int Count
    {
        get { return states.Count; }
    }

    public bool IsEmpty
    {
        get { return states.Count == 0; }
    }

    public void Add(BallState state)
    {
        if (!IsEmpty)
        {
            if (state.t < states[^1].t)
            {
                Debug.LogWarning("State time is not increasing in BallHistory.");
            }
        }

        states.Add(state);
    }

    public BallHistory Copy()
    {
        BallHistory history = new();
        foreach (var state in states)
        {
            history.Add(state.Copy());
        }

        return history;
    }

    public Tuple<Vector3[], int[], float[]> Vectorize()
    {
        if (IsEmpty)
        {
            return null;
        }

        int numStates = states.Count;

        Vector3[] rvws = new Vector3[numStates];
        int[] ss = new int[numStates];
        float[] ts = new float[numStates];

        for (int i = 0; i < numStates; i++)
        {
            rvws[i] = states[i].rvw[0];
            rvws[i] = states[i].rvw[1];
            rvws[i] = states[i].rvw[2];
            ss[i] = (int)states[i].state;
            ts[i] = states[i].t;
        }

        return new Tuple<Vector3[], int[], float[]>(rvws, ss, ts);
    }

    public static BallHistory FromVectorization(Tuple<Vector3[], float[], float[]> vectorization)
    {
        BallHistory history = new();

        if (vectorization == null)
        {
            return history;
        }

        int numStates = vectorization.Item1.GetLength(0);

        for (int i = 0; i < numStates; i++)
        {
            //history.Add(new BallState(vectorization.Item1[i], vectorization.Item2[i], vectorization.Item3[i]));
            history.Add(new BallState());
        }

        return history;
    }
}
