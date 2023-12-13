using UnityEngine;
using System.Collections.Generic;

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
