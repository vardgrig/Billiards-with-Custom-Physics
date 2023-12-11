using NUnit.Framework;
using System;
using UnityEngine;

public class CueSpecs
{
    public string Brand { get; set; } = "Predator";
    public float M { get; set; } = 0.567f; // 20oz
    public float Length { get; set; } = 1.4732f; // 58in
    public float TipRadius { get; set; } = 0.007f; // 14mm tip
    public float ButtRadius { get; set; } = 0.02f;

    public static CueSpecs Default()
    {
        return new CueSpecs();
    }
}

public class Cue
{
    public string Id = "cue_stick";
    public float V0 = 2f;
    public float phi = 0;
    public float theta = 0;
    public float a = 0;
    public float b = 0.25f;
    public string cue_ball_id = "cue";

    public CueSpecs specs = CueSpecs.Default();

    public override string ToString()
    {
        string[] lines = {
            $"<{this.GetType().Name} object at {this.GetHashCode()}>",
            $" ├── V0    : {this.V0}",
            $" ├── phi   : {this.phi}",
            $" ├── a     : {this.a}",
            $" ├── b     : {this.b}",
            $" └── theta : {this.theta}",
        };

        return string.Join("\n", lines) + "\n";
    }
    public Cue Copy()
    {
        return this.MemberwiseClone() as Cue;
    }
    public void ResetState()
    {
        this.SetState(V0=0, phi=0, theta=0, a=0,b=0);
    }
    public void SetState(float? V0 = null, float? phi = null, float? theta = null, float? a = null, float? b = null, string cueBallId = null)
    {
        if (V0.HasValue)
        {
            this.V0 = V0.Value;
        }
        if (phi.HasValue)
        {
            this.phi = phi.Value;
        }
        if (theta.HasValue)
        {
            this.theta = theta.Value;
        }
        if (a.HasValue)
        {
            this.a = a.Value;
        }
        if (b.HasValue)
        {
            this.b = b.Value;
        }
        if (cueBallId != null)
        {
            this.cue_ball_id = cueBallId;
        }
    }
    public Cue Default()
    {
        return new Cue();
    }
}

public class CueDatatypes : MonoBehaviour
{

}
