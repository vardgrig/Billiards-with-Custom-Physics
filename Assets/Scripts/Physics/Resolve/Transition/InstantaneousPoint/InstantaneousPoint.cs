using System;
using UnityEngine;

public class InstantaneousPoint : CoreStickBallCollision
{
    public Tuple<Vector3,Vector3> CueStrike(float m, float M, float R, float V0, float phi, float  theta, float a, float b,bool throttleEnglish)
    {
        a *= R;
        b *= R;

        if (throttleEnglish)
        {
            a *= Constants.english_fraction;
            b *= Constants.english_fraction;
        }

        phi *= Mathf.PI / 180;
        theta *= Mathf.PI / 180;

        var I = 2 / (5 * m * R * R);
        var c = Mathf.Sqrt(R * R - a * a - b * b);
        var numerator = 2 * M * V0;

        var temp = a * a 
                   + Mathf.Pow(b * Mathf.Cos(theta), 2) 
                   + Mathf.Pow(c * Mathf.Cos(theta), 2) 
                   - 2 * b * c * Mathf.Cos(theta) * Mathf.Sin(theta);

        var denominator = 1 + (m / M) + 5.0f / (2 * R * R) * temp;
        var F = numerator / denominator;

        var v_B = -F / m * new Vector3(0, 0, Mathf.Cos(theta));

        var vec_x = -c * Mathf.Sin(theta) + b * Mathf.Cos(theta);
        var vec_y = -a * Mathf.Cos(theta);
        var vec_z = a * Mathf.Sin(theta);

        var vec = new Vector3(vec_x, vec_y, vec_z);
        var w_B = F / I * vec;

        var rot_angle = phi + Mathf.PI / 2;
        var v_T = MathUtilities.CoordinateRotation(v_B, rot_angle);
        var w_T = MathUtilities.CoordinateRotation(w_B, rot_angle);

        return Tuple.Create(v_T, w_T);
    }

    public bool throttleEnglish;
    public override Tuple<Cue, Ball> Solve(Cue cue, Ball ball)
    {
        Tuple<Vector3, Vector3> v_w = CueStrike(
            ball.params_.m,
            cue.specs.M,
            ball.params_.R,
            cue.V0,
            cue.phi,
            cue.theta,
            cue.a,
            cue.b,
            throttleEnglish
            );
        var v = v_w.Item1;
        var w = v_w.Item2;

        var rvw = new Vector3[3]{ ball.state.rvw[0], v, w };
        var s = Constants.BallStates.Sliding;

        ball.state = new BallState(rvw, s, 0);

        return Tuple.Create(cue, ball);
    }
}
