using System.Collections.Generic;

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
