using UnityEngine;

public class PropertiesB2Cush
{
    public float GetBallCushionRestitution(Vector3[] rvw, float e_c)
    {
        return Mathf.Max(0.40f, 0.50f + 0.257f * rvw[1].x - 0.044f * rvw[1].x * rvw[1].x);
    }

    public float GetBallCushionFriction(Vector3[] rvw, float f_c)
    {
        // Calculate the angle in radians between the velocity vector rvw[1] and the reference frame direction.
        float ang = Vector3.Angle(rvw[1], new Vector3(1, 0, 0));

        if (ang > Mathf.PI)
            ang = Mathf.Abs(2.0f * Mathf.PI - ang);

        return f_c;
    }
}
