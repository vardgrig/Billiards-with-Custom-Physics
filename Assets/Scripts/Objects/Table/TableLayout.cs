using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class TableLayout
{
    public static CushionSegments CreateBilliardTableCushionSegment(ITableSpecs tableSpecs)
    {
        var h = tableSpecs.Height;
        Dictionary<string, LinearCushionSegment> linear = new Dictionary<string, LinearCushionSegment>()
        {
            { "3", new LinearCushionSegment()
            {
                Id = "3",
                P1 = new Vector3(0, h, 0),
                P2 = new Vector3(0, h, tableSpecs.l),
                Direction = (int)CushionDirection.SIDE2
            }
            },
            { "12", new LinearCushionSegment()
            {
                Id = "12",
                P1 = new Vector3(tableSpecs.w, h, tableSpecs.l),
                P2 = new Vector3(tableSpecs.w, h, 0),
                Direction = (int)CushionDirection.SIDE1
            } },
            { "9", new LinearCushionSegment()
            {
                Id = "9",
                P1 = new Vector3(0, h, tableSpecs.l),
                P2 = new Vector3(tableSpecs.w, h, tableSpecs.l),
                Direction = (int)CushionDirection.SIDE2
            } },
            { "18", new LinearCushionSegment()
            {
                Id = "18",
                P1 = new Vector3(0, h, 0),
                P2 = new Vector3(tableSpecs.w, h, 0),
                Direction = (int)CushionDirection.SIDE2
            }
            }
        };
        return new CushionSegments() { Linear = linear, Circular = new() };
    }
    public static CushionSegments CreatePocketTableCushionSegments(PocketTableSpecs specs)
    {
        var cw = specs.cushion_width;
        var ca = (specs.corner_pocket_angle + 45) * Mathf.PI / 180;
        var sa = specs.side_pocket_angle * Mathf.PI / 180;
        var pw = specs.corner_pocket_width;
        var sw = specs.side_pocket_width;
        var h = specs.cushion_height;
        var rc = specs.corner_jaw_radius;
        var rs = specs.side_jaw_radius;
        var dc = specs.corner_jaw_radius / Mathf.Tan((Mathf.PI / 2 + ca) / 2);
        var ds = specs.side_jaw_radius / Mathf.Tan((Mathf.PI / 2 + sa) / 2);

        var linear = new Dictionary<string, LinearCushionSegment>()
        {
            {"3" ,new LinearCushionSegment(){
                Id = "3",
                P1 = new(0, h, pw * Mathf.Cos(Mathf.PI / 4) + dc),
                P2 = new(0, h, (specs.l - sw) / 2 -ds),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"6" ,new LinearCushionSegment(){
                Id = "6",
                P1 = new(0, h, (specs.l + sw) / 2 + ds),
                P2 = new(0, h, -pw * Mathf.Cos(Mathf.PI / 4) + specs.l - dc),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"15" ,new LinearCushionSegment(){
                Id = "15",
                P1 = new(specs.w, h, pw * Mathf.Cos(Mathf.PI / 4) + dc),
                P2 = new(specs.w, h, (specs.l - sw) / 2 -ds),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"12" ,new LinearCushionSegment(){
                Id = "12",
                P1 = new(specs.w, h, (specs.l + sw) / 2 + ds),
                P2 = new(specs.w, h, -pw * Mathf.Cos(Mathf.PI/4) + specs.l - dc),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"18" ,new LinearCushionSegment(){
                Id = "18",
                P1 = new(pw * Mathf.Cos(Mathf.PI / 4) + dc, h, 0),
                P2 = new(-pw * Mathf.Cos(Mathf.PI / 4) + specs.w - dc, h, 0),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"9" ,new LinearCushionSegment(){
                Id = "9",
                P1 = new(pw * Mathf.Cos(Mathf.PI / 4) + dc, h, specs.l),
                P2 = new(-pw * Mathf.Cos(Mathf.PI / 4) + specs.w - dc, h, specs.l),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"5" ,new LinearCushionSegment(){
                Id = "5",
                P1 = new(-cw, h, (specs.l + sw) / 2 - cw * Mathf.Sin(sa)),
                P2 = new(-ds * Mathf.Cos(sa), h, (specs.l + sw) / 2 - ds * Mathf.Sin(sa)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"4" ,new LinearCushionSegment(){
                Id = "4",
                P1 = new(-cw, h, (specs.l - sw) / 2 + cw * Mathf.Sin(sa)),
                P2 = new(-dc * Mathf.Cos(sa), h, (specs.l - sw) / 2 + ds * Mathf.Sin(sa)),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"13" ,new LinearCushionSegment(){
                Id = "13",
                P1 = new(specs.w + cw, h, (specs.l + sw) / 2 - cw * Mathf.Sin(sa)),
                P2 = new(specs.w + ds * Mathf.Cos(sa), h, (specs.l + sw) / 2 - ds * Mathf.Sin(sa)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"14" ,new LinearCushionSegment(){
                Id = "14",
                P1 = new(specs.w + cw, h, (specs.l - sw) / 2 + cw * Mathf.Sin(sa)),
                P2 = new(specs.w + ds * Mathf.Cos(sa), h, (specs.l - sw) / 2 + ds * Mathf.Sin(sa)),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"1" ,new LinearCushionSegment(){
                Id = "1",
                P1 = new(pw * Mathf.Cos(Mathf.PI / 4) - cw * Mathf.Tan(ca), h, -cw),
                P2 = new(pw * Mathf.Cos(Mathf.PI / 4) - dc * Mathf.Sin(ca), h, -dc * Mathf.Cos(ca)),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"2" ,new LinearCushionSegment(){
                Id = "2",
                P1 = new(-cw, h, pw * Mathf.Cos(Mathf.PI / 4) - cw * Mathf.Tan(ca)),
                P2 = new(-dc * Mathf.Cos(ca), h, pw * Mathf.Cos(Mathf.PI / 4) - dc * Mathf.Sin(ca)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"8" ,new LinearCushionSegment(){
                Id = "8",
                P1 = new(pw * Mathf.Cos(Mathf.PI / 4) - cw * Mathf.Tan(ca), h, cw + specs.l),
                P2 = new(pw * Mathf.Cos(Mathf.PI / 4) - dc * Mathf.Sin(ca), h, specs.l + dc * Mathf.Cos(ca)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"7" ,new LinearCushionSegment(){
                Id = "7",
                P1 = new(-cw, h, -pw * Mathf.Cos(Mathf.PI / 4) + cw * Mathf.Tan(ca) + specs.l),
                P2 = new(-dc * Mathf.Cos(ca), h, -pw * Mathf.Cos(Mathf.PI / 4) + specs.l  + dc * Mathf.Sin(ca)),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"11" ,new LinearCushionSegment(){
                Id = "11",
                P1 = new(cw + specs.w, h, -pw * Mathf.Cos(Mathf.PI / 4) + cw * Mathf.Tan(ca) + specs.l),
                P2 = new(specs.w + dc * Mathf.Cos(ca), h, -pw * Mathf.Cos(Mathf.PI / 4) + specs.l + dc * Mathf.Sin(ca)),
                Direction = (int)CushionDirection.SIDE2
            }},
            {"10" ,new LinearCushionSegment(){
                Id = "10",
                P1 = new(-pw * Mathf.Cos(Mathf.PI / 4) + cw * Mathf.Tan(ca) + specs.w, h, cw + specs.l),
                P2 = new(-pw * Mathf.Cos(Mathf.PI / 4) + specs.w + dc * Mathf.Sin(ca), h, specs.l + dc * Mathf.Cos(ca)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"16" ,new LinearCushionSegment(){
                Id = "16",
                P1 = new(cw + specs.w, h, pw * Mathf.Cos(Mathf.PI/4) + cw * Mathf.Tan(ca)),
                P2 = new(specs.w + dc * Mathf.Cos(ca), h, pw * Mathf.Cos(Mathf.PI / 4) - cw * Mathf.Tan(ca)),
                Direction = (int)CushionDirection.SIDE1
            }},
            {"17" ,new LinearCushionSegment(){
                Id = "17",
                P1 = new(-pw * Mathf.Cos(Mathf.PI / 4) + cw * Mathf.Tan(ca) + specs.w, h, -cw),
                P2 = new(-pw * Mathf.Cos(Mathf.PI / 4) + specs.w + dc * Mathf.Sin(ca), h, -dc * Mathf.Cos(ca)),
                Direction = (int)CushionDirection.SIDE2
            }}
        };
        var circular = new Dictionary<string, CircularCushionSegment>()
        {
            {"1t", new CircularCushionSegment()
            {
                Id = "1t",
                Center = new Vector3(pw * Mathf.Cos(Mathf.PI/4)+dc,h,-rc),
                Radius = rc
            }},
            {"2t", new CircularCushionSegment()
            {
                Id = "2t",
                Center = new Vector3(-rc,h,pw * Mathf.Cos(Mathf.PI/4)+dc),
                Radius = rc
            }},
            {"4t", new CircularCushionSegment()
            {
                Id = "4t",
                Center = new Vector3(-rs, h, specs.l / 2 - sw / 2 - ds),
                Radius = rs
            }},
            {"5t", new CircularCushionSegment()
            {
                Id = "5t",
                Center = new Vector3(-rs, h, specs.l / 2 + sw / 2 + ds),
                Radius = rs
            }},
            {"7t", new CircularCushionSegment()
            {
                Id = "7t",
                Center = new Vector3(-rc, h, specs.l - (pw * Mathf.Cos(Mathf.PI/4)+dc)),
                Radius = rc
            }},
            {"8t", new CircularCushionSegment()
            {
                Id = "8t",
                Center = new Vector3(pw * Mathf.Cos(Mathf.PI/4)+dc,h,specs.l + rc),
                Radius = rc
            }},
            {"10t", new CircularCushionSegment()
            {
                Id = "10t",
                Center = new Vector3(specs.w - pw * Mathf.Cos(Mathf.PI / 4) - dc, h, -specs.l + rc),
                Radius = rc
            }},
            {"11t", new CircularCushionSegment()
            {
                Id = "11t",
                Center = new Vector3(specs.w + rc, h, specs.l - (pw * Mathf.Cos(Mathf.PI / 4) + dc)),
                Radius = rc
            }},
            {"13t", new CircularCushionSegment()
            {
                Id = "13t",
                Center = new Vector3(specs.w + rs, h, specs.l / 2 + sw / 2 + ds),
                Radius = rs
            }},
            {"14t", new CircularCushionSegment()
            {
                Id = "14t",
                Center = new Vector3(specs.w + rs, h, specs.l / 2 - sw / 2 - ds),
                Radius = rs
            }},
            {"16t", new CircularCushionSegment()
            {
                Id = "16t",
                Center = new Vector3(specs.w + rc, h, pw * Mathf.Cos(Mathf.PI / 4) + dc),
                Radius = rc
            }},
            {"17t", new CircularCushionSegment()
            {
                Id = "17t",
                Center = new Vector3(specs.w - pw * Mathf.Cos(Mathf.PI/4) - dc, h, -rc),
                Radius = rc
            }}
        };
        return new CushionSegments() { Circular = circular, Linear = linear };
    }
    public static Dictionary<string, Pocket> CreatePocketTablePockets(PocketTableSpecs specs)
    {
        var pw = specs.corner_pocket_width;
        var cr = specs.corner_pocket_radius;
        var sr = specs.side_pocket_radius;
        var cd = specs.corner_pocket_depth;
        var sd = specs.side_pocket_depth;

        var cD = cr + cd - pw / 2;
        var sD = sr + sd;

        var pockets = new Dictionary<string, Pocket>()
        {
            {"lb", new Pocket(){
                Id = "lb",
                Center = new(-cD / Mathf.Sqrt(2), 0, -cD / Mathf.Sqrt(2)),
                Radius = cr,
            }},
            {"lc", new Pocket(){
                Id = "lc",
                Center = new(-sD, 0, specs.l / 2),
                Radius = sr,
            }},
            {"lt", new Pocket(){
                Id = "lt",
                Center = new(-cD / Mathf.Sqrt(2), 0, specs.l + cd / Mathf.Sqrt(2)),
                Radius = cr,
            }},
            {"rb", new Pocket(){
                Id = "rb",
                Center = new(specs.w + cd / Mathf.Sqrt(2), 0, -cD / Mathf.Sqrt(2)),
                Radius = cr,
            }},
            {"rc", new Pocket(){
                Id = "rc",
                Center = new(specs.w + sD, 0, specs.l / 2),
                Radius = sr,
            }},
            {"rt", new Pocket(){
                Id = "rt",
                Center = new(specs.w + cd / Mathf.Sqrt(2), 0, specs.l + cD / Mathf.Sqrt(2)),
                Radius = cr,
            }}
        };
        return pockets;
    }
}
