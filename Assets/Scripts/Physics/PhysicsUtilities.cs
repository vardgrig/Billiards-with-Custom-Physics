using UnityEngine;
using static Constants;

public static class PhysicsUtilities
{
    public static Vector3 Rel_Velocity(Vector3[] rvw, float R)
    {
        Vector3 v = rvw[1]; // Assuming index 1 is for velocity
        Vector3 w = rvw[2]; // Assuming index 2 is for angular velocity

        // Create a vector representing [0.0, 0.0, 1.0]
        Vector3 zeroVector = new(0.0f, 0.0f, 1.0f);

        // Compute the cross product between zeroVector and w
        Vector3 crossProduct = Vector3.Cross(zeroVector, w);

        // Compute the relative velocity
        Vector3 relativeVelocity = v + R * crossProduct;

        return relativeVelocity;
    }

    public static Vector3 GetUVec(Vector3[] rvw, float phi, float R, BallStates s)
    {
        if (s == BallStates.Rolling)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        Vector3 relVel = ComputeRelativeVelocity(rvw, R);

        if (relVel == Vector3.zero)
        {
            return new Vector3(1.0f, 0.0f, 0.0f);
        }

        Vector3 unitRelVel = relVel.normalized;

        Vector3 rotatedUnitRelVel = CoordinateRotation(unitRelVel, -phi);

        return rotatedUnitRelVel;
    }

    private static Vector3 ComputeRelativeVelocity(Vector3[] rvw, float R)
    {
        return Vector3.zero; 
    }

    private static Vector3 CoordinateRotation(Vector3 vector, float phi)
    {
        return Vector3.zero;
    }
    public static float Get_Slide_Time(Vector3[] rvw, float R, float u_s, float g)
    {
        return 2 * MathUtilities.Norm3D(Rel_Velocity(rvw,R)) / (7 * u_s * g);
    }
    public static float Get_Roll_Time(Vector3[] rvw, float u_r, float g)
    {
        Vector3 v = rvw[1];
        return MathUtilities.Norm3D(v) / (u_r * g);
    }
    public static float Get_Spin_Time(Vector3[] rvw, float R, float u_sp, float g)
    {
        Vector3 w = rvw[2];
        return Mathf.Abs(w[2]) * 2 / 5 * R / u_sp / g;
    }
    public static float Get_Ball_Energy(Vector3[] rvw, float R, float m)
    {
        //Linear
        float LKE = m * Mathf.Pow(MathUtilities.Norm3D(rvw[1]), 2) / 2;

        //Rotational
        float I = 2 / 5 * m * R * R;
        float RKE = I * Mathf.Pow(MathUtilities.Norm3D(rvw[1]), 2) / 2;

        return LKE + RKE;
    }

    public static bool IsOverlapping(Vector3[] rvw1, Vector3[] rvw2, float R1, float R2)
    {
        return MathUtilities.Norm3D(rvw1[0] - rvw2[0]) < (R1 + R2);
    }
}
