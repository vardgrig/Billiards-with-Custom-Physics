using UnityEngine;
using System;
using System.Collections.Generic;
public enum CushionDirection
{
    SIDE1,
    SIDE2,
    BOTH
}

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

public class CircularCushionSegment
{
    public string Id { get; set; }
    public Vector3 Center { get; set; }
    public float Radius { get; set; }

    public float Height => Center[2];
    public float A => Center[0];
    public float B => Center[2];

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

public class CushionSegments
{
    public Dictionary<string, LinearCushionSegment> Linear { get; set; }
    public Dictionary<string, CircularCushionSegment> Circular { get; set; }

    public CushionSegments Copy()
    {
        return new CushionSegments
        {
            Linear = new Dictionary<string, LinearCushionSegment>(Linear),
            Circular = new Dictionary<string, CircularCushionSegment>(Circular)
        };
    }
}

public class Pocket
{
    public string Id { get; set; }
    public Vector3 Center { get; set; }
    public float Radius { get; set; }
    public float Depth { get; set; } = 0.08f;
    public HashSet<string> Contains { get; set; } = new HashSet<string>();

    public float A => Center[0];
    public float B => Center[2];

    public Vector3 PottingPoint
    {
        get
        {
            float x = Center[0];
            float z = Center[2];

            if (Id[0] == 'l')
            {
                x += Radius;
            }
            else
            {
                x -= Radius;
            }

            if (Id[1] == 'b')
            {
                z += Radius;
            }
            else if (Id[1] == 't')
            {
                z -= Radius;
            }

            return new Vector3 (x, 0, z);
        }
    }

    public void Add(string ballId)
    {
        Contains.Add(ballId);
    }

    public void Remove(string ballId)
    {
        Contains.Remove(ballId);
    }

    public Pocket Copy()
    {
        return new Pocket
        {
            Id = Id,
            Center = Center,
            Radius = Radius,
            Depth = Depth,
            Contains = new HashSet<string>(Contains)
        };
    }

    public static Pocket Dummy()
    {
        return new Pocket
        {
            Id = "dummy",
            Center = Vector3.zero,
            Radius = 10
        };
    }
}

public class TableComponents : MonoBehaviour
{

}
