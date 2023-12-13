using System.Collections.Generic;

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
    private float _l = 1.9812f;
    private float _w = 1.9812f / 2;
    private float _cushion_width = 2 * 2.54f / 100;
    private float _cushion_height = 0.64f * 2 * 0.028575f;
    private TableType _TableType = TableType.POCKET;
    public float _Height = 0.708f;
    public float _LightsHeight = 1.99f;
    public TableModelDescr _ModelDescr = TableModelDescr.Null();

    public float l { get { return _l; } set { _l = value; } }
    public float w { get { return _w; } set { _w = value; } }
    public float cushion_width { get { return _cushion_width; } set { _cushion_width = value; } }
    public float cushion_height { get => _cushion_height; set => _cushion_height = value; }
    public TableType TableType { get => _TableType; set => _TableType = value; }
    public float Height { get => _Height; set => _Height = value; }
    public float LightsHeight { get => _LightsHeight; set => _LightsHeight = value; }
    public TableModelDescr ModelDescr { get => _ModelDescr; set => _ModelDescr = value; }

    public CushionSegments CreateCushionSegments()
    {
        return TableLayout.CreateBilliardTableCushionSegment(this);
    }
    public Dictionary<string, Pocket> CreatePockets()
    {
        return TableLayout.CreatePocketTablePockets(this);
    }
}
