using System;
using UnityEngine;

public class BallOrientation
{
    public Vector4 pos;
    public Vector4 sphere;

    public BallOrientation()
    {
        float[] quat = new float[4];
        System.Random rand = new();
        for (int i = 0; i < 4; i++)
        {
            quat[i] = 2 * (float)rand.NextDouble() - 1;
        }

        float norm = Mathf.Sqrt(quat[0] * quat[0] + quat[1] * quat[1] + quat[2] * quat[2] + quat[3] * quat[3]);

        float q0 = quat[0] / norm;
        float qx = quat[1] / norm;
        float qy = quat[2] / norm;
        float qz = quat[3] / norm;

        this.pos = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        this.sphere = new Vector4(q0, qx, qy, qz);

        Debug.Log("Default Constructor for BallOrientation");
    }

    public BallOrientation Copy()
    {
        // Since the class is frozen and attributes are immutable, just return self
        return this;
    }
    public BallOrientation(Vector4 pos, Vector4 sphere)
    {
        this.pos = pos;
        this.sphere = sphere;
    }
}
