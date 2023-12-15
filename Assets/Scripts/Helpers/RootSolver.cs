using System;
using System.Numerics;
using UnityEngine;

public static class RootSolver
{
    public static Tuple<Complex, Complex> SolveQuadratic(float a, float b, float c)
    {
        if(a == 0)
        {
            Complex u = -c / b;
            return Tuple.Create(u, u);
        }
        var bp = b / 2;
        var delta = bp * bp - a * c;
        Complex u1 = (-bp - Mathf.Sqrt(delta)) / a;
        Complex u2 = -u1 - b / a;
        return Tuple.Create(u1, u2);
    }
}
