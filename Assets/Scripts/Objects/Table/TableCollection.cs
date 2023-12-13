using System.Collections.Generic;

public class TableCollection
{
    Dictionary<TableType, TableNameEnum> defaultMap = new()
    {
        {
            TableType.POCKET, TableNameEnum.SEVEN_FOOT_SHOWOOD
        }
    };
    Dictionary<TableNameEnum, ITableSpecs> TABLE_SPECS = new()
    {
        {
            TableNameEnum.SEVEN_FOOT_SHOWOOD, new PocketTableSpecs()
            {
                l = 1.9812f,
                w = 1.9812f/2,
                cushion_width = 2*2.54f / 100,
                cushion_height = 0.64f * 2 * 0.028575f,
                corner_pocket_width = 0.118f,
                corner_pocket_angle = 5.3f,
                corner_pocket_depth = 0.0398f,
                corner_pocket_radius = 0.124f/2,
                corner_jaw_radius = 0.0419f/2,
                side_jaw_radius = 0.0159f/2,
                side_pocket_angle = 7.14f,
                side_pocket_radius = 0.129f/2,
                side_pocket_depth = 0.00437f,
                side_pocket_width = 0.137f,
                Height = 0.708f,
                LightsHeight = 1.99f,
                ModelDescr = new TableModelDescr(){Name = "seven_foot_showood" }
            }
        }
    };
    public ITableSpecs PreBuiltSpecs(TableNameEnum name)
    {
        return TABLE_SPECS[name];
    }
    public ITableSpecs GetDefaultSpecs(TableType tableType)
    {
        return PreBuiltSpecs(defaultMap[tableType]);
    }
}