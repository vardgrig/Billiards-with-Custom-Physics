using System;
using UnityEngine;
public class ModelHan2005 : MonoBehaviour
{
    PropertiesB2Cush properties;
    private Vector3[] Han2005(Vector3[] rvw, Vector3 normal, float R, float m, float h, float e_c, float f_c)
    {
        normal = Vector3.Dot(normal, rvw[1]) > 0 ? normal : -normal;
        float psi = Vector3.Angle(normal, Vector3.right);
        Vector3[] rvw_R = MathUtilities.T(MathUtilities.CoordinateRotation(MathUtilities.T(rvw),-psi));

        float phi = Vector3.Angle(rvw_R[1], Vector3.right) % (2 * Mathf.PI);
        float e = properties.GetBallCushionRestitution(rvw_R, e_c);
        float mu = properties.GetBallCushionFriction(rvw_R, f_c);

        float theta_a = Mathf.Asin(h / R - 1);

        float sx = rvw_R[1].x * Mathf.Sin(theta_a) - rvw_R[1].y * Mathf.Cos(theta_a) + R * rvw_R[2].z;
        float sz = -rvw_R[1].z - R * rvw_R[1].y * Mathf.Cos(theta_a) + R * rvw_R[1].x * Mathf.Sin(theta_a);
        float c = rvw_R[1].x * Mathf.Cos(theta_a);

        float I = 2f / 5f * m * R * R;
        float A = 7f / 2f / m;
        float B = 1f / m;


        float PzE = (1 + e) * c / B;
        float PzS = Mathf.Sqrt(sx * sx + sz * sz) / A;

        float PX, PY, PZ;

        if (PzS <= PzE)
        {
            PX = -sx / A * Mathf.Sin(theta_a) - (1 + e) * c / B * Mathf.Cos(theta_a);
            PZ = sz / A;
            PY = sx / A * Mathf.Cos(theta_a) - (1 + e) * c / B * Mathf.Sin(theta_a);
        }
        else
        {
            PX = -mu * (1 + e) * c / B * Mathf.Cos(phi) * Mathf.Sin(theta_a) - (1 + e) * c / B * Mathf.Cos(theta_a);
            PZ = mu * (1 + e) * c / B * Mathf.Sin(phi);
            PY = mu * (1 + e) * c / B * Mathf.Cos(phi) * Mathf.Cos(theta_a) - (1 + e) * c / B * Mathf.Sin(theta_a);
        }

        rvw_R[1].x += PX / m;
        rvw_R[1].z += PZ / m;

        rvw_R[2].x += -R / I * PZ * Mathf.Sin(theta_a);
        rvw_R[2].z += R / I * (PX * Mathf.Sin(theta_a) - PZ * Mathf.Cos(theta_a));
        rvw_R[2].y += R / I * PZ * Mathf.Cos(theta_a);

        rvw = MathUtilities.T(MathUtilities.CoordinateRotation(MathUtilities.T(rvw_R), psi));

        return rvw;
    }
    Tuple<Ball, CushionInfo> Solve(Ball ball, CushionInfo cushion)
    {
        var rvw = Han2005(ball.state.rvw, cushion.GetNormal(ball.state.rvw), ball.params_.R, ball.params_.m, cushion.height, ball.params_.e_c, ball.params_.f_c);
        ball.state = new BallState(rvw, Constants.BallStates.Sliding, 0);
        return Tuple.Create(ball, cushion);
    }
}