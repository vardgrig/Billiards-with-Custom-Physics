using System.Collections.Generic;
using UnityEngine;
public enum TableType
{
    POCKET,
    BILLAIRD
}
public class TableModelDescr
{
    public string Name { get; set; }

    public string Path
    {
        get
        {
            // Implement the path property logic based on the provided Python code
            return "";
        }
    }

    public static TableModelDescr Null()
    {
        return new TableModelDescr { Name = "null" };
    }
}
public class BilliardTableSpecs : ITableSpecs
{
    public float l => 3.05f;
    public float w => 3.05f / 2;

    public float cushion_width => 2 * 2.54f / 100;
    public float cushion_height => 0.64f * 2 * 0.028575f;

    public TableType TableType => TableType.BILLAIRD;
    public float Height => 0.708f;
    public float LightsHeight => 1.99f;
    public TableModelDescr ModelDescr => TableModelDescr.Null();

    public CushionSegments CreateCushionSegments()
    {
        return TableLayout.CreateBilliardTableCushionSegment(this);
    }

    public Dictionary<string, Pocket> CreatePockets()
    {
        return new Dictionary<string, Pocket>();
    }
}
public class TableSpecs : MonoBehaviour
{

}