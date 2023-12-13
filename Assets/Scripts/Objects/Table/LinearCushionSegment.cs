using UnityEngine;

public class LinearCushionSegment
{
    public string Id { get; set; }
    public Vector3 P1 { get; set; }
    public Vector3 P2 { get; set; }
    public int Direction { get; set; } = (int)CushionDirection.BOTH;

    public float Height => P1[2];

    public float LX
    {
        get
        {
            float p1x = P1[0];
            float p1y = P1[1];
            float p2x = P2[0];
            float p2y = P2[1];

            return (p2x - p1x) == 0 ? 1 : -(p2y - p1y) / (p2x - p1x);
        }
    }

    public float LZ => (P2[0] - P1[0]) == 0 ? 0 : 1;

    public float L0
    {
        get
        {
            float p1x = P1[0];
            float p1y = P1[1];
            float p2x = P2[0];
            float p2y = P2[1];

            return (p2x - p1x) == 0 ? -p1x : (p2y - p1y) / (p2x - p1x) * p1x - p1y;
        }
    }

    public Vector3 Normal => MathUtilities.UnitVector(new Vector3 (LX, 0, LZ ));

    public Vector3 GetNormal(float[,] rvw)
    {
        return Normal;
    }

    public LinearCushionSegment Copy()
    {
        return this;
    }

    public static LinearCushionSegment Dummy()
    {
        return new LinearCushionSegment
        {
            Id = "dummy",
            P1 = new Vector3(0, 1, 0),
            P2 = new Vector3(1, 1, 1)
        };
    }
}
