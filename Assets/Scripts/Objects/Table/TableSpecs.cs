using System.Collections.Generic;
using UnityEngine;

public interface ITableSpecs
{
    float l { get; }
    float w {  get; }
    float cushion_width { get; }
    float cushion_height { get; }
    TableType TableType { get; }
    float Height { get; }
    float LightsHeight { get; }
    TableModelDescr ModelDescr { get; }
    CushionSegments CreateCushionSegments();
    Dictionary<string, Pocket> CreatePockets();
}
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
public class PocketTableSpecs : ITableSpecs
{
    public float corner_pocket_width = 0.118f;
    public float corner_pocket_angle = 5.3f; //degrees
    public float corner_pocket_depth = 0.0398f;
    public float corner_pocket_radius = 0.124f / 2;
    public float corner_jaw_radius = 0.0419f / 2;
    public float side_pocket_width = 0.137f;
    public float side_pocket_angle = 7.14f; //degrees
    public float side_pocket_depth = 0.00437f;
    public float side_pocket_radius = 0.129f / 2;
    public float side_jaw_radius = 0.0159f / 2;
    public float l => 1.9812f;
    public float w => 1.9812f / 2;
    public float cushion_width => 2 * 2.54f / 100;
    public float cushion_height => 0.64f * 2 * 0.028575f;
    public TableType TableType => TableType.POCKET;
    public float Height => 0.708f;
    public float LightsHeight => 1.99f;
    public TableModelDescr ModelDescr => TableModelDescr.Null();
    public CushionSegments CreateCushionSegments()
    {
        return TableLayout.CreateBilliardTableCushionSegment(this);
    }
    public Dictionary<string, Pocket> CreatePockets()
    {
        return TableLayout.CreatePocketTablePockets(this);
    }
}
public class TableSpecs : MonoBehaviour
{

}