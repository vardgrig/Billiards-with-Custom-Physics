using System.Collections.Generic;

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
