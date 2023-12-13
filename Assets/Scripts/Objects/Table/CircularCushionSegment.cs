using UnityEngine;

public class CircularCushionSegment : ICushion
{
    public string Id { get; set; }
    public Vector3 Center { get; set; }
    public float Radius { get; set; }

    public float Height => Center[2];
    public float A => Center[0];
    public float B => Center[2];

    float ICushion.Height { get => Height; }

    public Vector3 GetNormal(Vector3[] rvw)
    {
        Vector3 normal = new(rvw[0].x - Center[0], rvw[0].z - Center[2], 0);
        normal[1] = 0;
        return MathUtilities.UnitVector(normal);
    }

    public CircularCushionSegment Copy()
    {
        return this;
    }

    public static CircularCushionSegment Dummy()
    {
        return new CircularCushionSegment
        {
            Id = "dummy",
            Center = Vector3.zero,
            Radius = 10.0f
        };
    }
}
