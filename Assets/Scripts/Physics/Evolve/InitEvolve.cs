using System;
using UnityEngine;

public static class InitEvolve
{
    public static Tuple<Vector3[], Constants.BallStates> Evolve_Ball_Motion(Constants.BallStates state, Vector3[] rvw, float R, float m, float u_s, float u_sp, float u_r, float g, float t)
    {
        if (state == Constants.BallStates.Stationary || state == Constants.BallStates.Pocketed)
            return new Tuple<Vector3[], Constants.BallStates>(rvw, state);

        if (state == Constants.BallStates.Sliding)
        {
            var dtau_E_slide = PhysicsUtilities.Get_Slide_Time(rvw, R, u_s, g);

            if (t >= dtau_E_slide)
            {
                rvw = Evolve_Slide_State(rvw, R, m, u_s, u_sp, g, t);
                state = Constants.BallStates.Rolling;
                t -= dtau_E_slide;
            }
            else
                return new Tuple<Vector3[], Constants.BallStates>(Evolve_Slide_State(rvw, R, m, u_s, u_sp, g, t), Constants.BallStates.Sliding);
        }

        if (state == Constants.BallStates.Spinning)
        {
            var dtau_E_spin = PhysicsUtilities.Get_Spin_Time(rvw, R, u_sp, g);

            if (t >= dtau_E_spin)
                return new Tuple<Vector3[], Constants.BallStates>(Evolve_Perpendicular_Spin_State(rvw, R, u_sp, g, dtau_E_spin), Constants.BallStates.Stationary);
            else
                return new Tuple<Vector3[], Constants.BallStates>(Evolve_Perpendicular_Spin_State(rvw, R, u_sp, g, t), Constants.BallStates.Spinning);
        }

        Debug.Log("Evolve: Added last case alert!");
        return new Tuple<Vector3[], Constants.BallStates>(rvw, state);

    }
    public static Vector3[] Evolve_Slide_State(Vector3[] rvw, float R, float m, float u_s, float u_sp, float g, float t)
    {
        if (t == 0)
            return rvw;

        var phi = MathUtilities.Angle(rvw[1]);
        var rvw_B0 = MathUtilities.Transpose(MathUtilities.CoordinateRotation(MathUtilities.Transpose(rvw), -phi));

        var u_0 = MathUtilities.CoordinateRotation(MathUtilities.UnitVector(PhysicsUtilities.Rel_Velocity(rvw,R)), -phi);

        var rvw_B = new Vector3[3];
        rvw_B[0].x = rvw_B0[1].y * t - 0.5f * u_s * g * t * t * u_0[0];
        rvw_B[0].y = -0.5f * u_s * g * t * t * u_0[1];
        rvw_B[0].z = 0;
        rvw_B[1] = rvw_B0[1] - u_s * g * t * u_0;
        rvw_B[2] = rvw_B0[2] - 5 / 2 / R * u_s * g * t * MathUtilities.Cross(u_0, new Vector3(0, 0, 1));
        rvw_B[2].z = rvw_B0[2].z;
        rvw_B = Evolve_Perpendicular_Spin_State(rvw_B, R, u_sp, g, t);

        var rvw_T = MathUtilities.Transpose(MathUtilities.CoordinateRotation(MathUtilities.Transpose(rvw_B), -phi));
        rvw_T[0] += rvw[0];
        return rvw_T;
    }
    public static Tuple<Vector3[], Constants.BallStates> Evolve_State_Motion(Constants.BallStates state, Vector3[] rvw, float R, float m, float u_s, float u_sp, float u_r, float g, float t)
    {
        if (state == Constants.BallStates.Stationary || state == Constants.BallStates.Pocketed)
            return new Tuple<Vector3[], Constants.BallStates>(rvw, state);
        else if (state == Constants.BallStates.Sliding)
            return new Tuple<Vector3[], Constants.BallStates>(Evolve_Slide_State(rvw, R, m, u_s, u_sp, g, t), Constants.BallStates.Sliding);
        else if (state == Constants.BallStates.Rolling)
            return new Tuple<Vector3[], Constants.BallStates>(Evolve_Roll_State(rvw, R, u_r, u_sp, g, t), Constants.BallStates.Rolling);
        else if (state == Constants.BallStates.Spinning)
            return new Tuple<Vector3[], Constants.BallStates>(Evolve_Perpendicular_Spin_State(rvw, R, u_sp, g, t), Constants.BallStates.Spinning);
        return new Tuple<Vector3[], Constants.BallStates>(rvw, state);
    }
    public static Vector3[] Evolve_Roll_State(Vector3[] rvw, float R, float u_r, float u_sp, float g, float t)
    {
        if (t == 0)
            return rvw;

        var r0 = rvw[0];
        var v0 = rvw[1];
        var w0 = rvw[2];
        var v0_hat = MathUtilities.UnitVector(v0);
        var r = r0 + v0 * t - 0.5f * u_r * g * t * t * v0_hat;
        var v = v0 - u_r * g * t * v0_hat;
        var w = MathUtilities.CoordinateRotation(v / R, Mathf.PI / 2);

        var temp = Evolve_Perpendicular_Spin_State(rvw, R, u_sp, g, t);

        //w.z = temp[2][2];
        w[2] = temp[2][2];

        var new_rvw = new Vector3[rvw.Length];
        new_rvw[0] = r;
        new_rvw[1] = v;
        new_rvw[2] = w;

        return new_rvw;

    }
    public static Vector3[] Evolve_Perpendicular_Spin_State(Vector3[] rvw, float R, float u_sp, float g, float t)
    {
        rvw[2][2] = Evolve_Perpendicular_Spin_Component(rvw[2][2], R, u_sp, g, t);
        return rvw;
    }
    public static float Evolve_Perpendicular_Spin_Component(float wz, float R, float u_sp, float g, float t)
    {
        if (t == 0)
            return wz;

        if (Mathf.Abs(wz) < Mathf.Epsilon)
            return wz;

        var aplha = 5 * u_sp * g / (2 * R);

        if (t > Mathf.Abs(wz) / aplha)
            t = Mathf.Abs(wz) / aplha;

        var sign = wz > 0 ? 1 : -1;

        var wz_final = wz - sign * aplha * t;
        return wz_final;
    }
}