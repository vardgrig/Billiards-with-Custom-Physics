using UnityEngine;

public class BallParams
{
    public float m = 0.170097f;                // Mass (kilograms)
    public float R = 0.028575f;                // Radius (meters)
    public float u_s = 0.2f;                   // Coefficient of sliding friction
    public float u_r = 0.01f;                  // Coefficient of rolling friction
    public float u_sp_proportionality = 10 * 2f / 5f / 9f;  // Spinning friction proportionality constant
    public float e_c = 0.85f;                 // Cushion coefficient of restitution
    public float f_c = 0.2f;                  // Cushion coefficient of friction
    public float g = 9.8f;                    // Gravitational constant (m/s^2)

    public float u_sp
    {
        get { return u_sp_proportionality * R; }
    }

    public BallParams Copy()
    {
        // Since the class is frozen and attributes are immutable, just return self
        return this;
    }

    public BallParams()
    {
        Debug.Log("Default Constructor for BallParams");
    }
}
