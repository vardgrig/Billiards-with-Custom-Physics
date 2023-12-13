using System;
using System.Collections.Generic;

public class Table
{
    public CushionSegments cushionSegments;
    public Dictionary<string, Pocket> pockets;
    public TableType tableType;
    public TableModelDescr tableModelDescr;
    public float height = 0.708f;
    public float lightHeight = 1.99f;
    public float W
    {
        get
        {
            var x2 = cushionSegments.Linear["12"].P1[0];
            var x1 = cushionSegments.Linear["3"].P1[0];
            return x2 - x1;
        }
    }
    public float L
    {
        get
        {
            var y2 = cushionSegments.Linear["9"].P1[1];
            var y1 = cushionSegments.Linear["18"].P1[1];
            return y2 - y1;
        }
    }
    public Tuple<float, float> Center()
    {
        return Tuple.Create(W / 2, L / 2);
    }
    public static Table FromTableSpecs(ITableSpecs specs)
    {
        return new Table()
        {
            cushionSegments = specs.CreateCushionSegments(),
            pockets = specs.CreatePockets(),
            tableType = specs.TableType,
            tableModelDescr = specs.ModelDescr,
            height = specs.Height,
            lightHeight = specs.LightsHeight
        };
    }
    public Table PreBuilt(TableNameEnum name)
    {
        TableCollection tables = new();
        return FromTableSpecs(tables.PreBuiltSpecs(name));
    }
}