using System.Collections.Generic;
using System.Linq;

public class MultiSystem
{
    public List<MySystem> multisystem = new();
    public int? activeIndex;
    public int Len => multisystem.Count;
    public MySystem GetItem(int index) => multisystem[index];
    public MySystem Active => multisystem[activeIndex.Value];
    int MaxIndex => multisystem.Count;
    public void Reset()
    {
        activeIndex = 0;
        multisystem.Clear();
    }
    bool Empty => !(multisystem.Count == 0);
    public void Append(MySystem system)
    {
        if (this.Empty)
            activeIndex = 0;

        multisystem.Append(system);
    }
    public void Extend(List<MySystem> systems)
    {
        if(Empty)
            activeIndex = 0;

        multisystem.AddRange(systems);
    }
    public void SetActive(int i)
    {
        if (activeIndex.HasValue)
        {
            var table = this.Active.table;
            activeIndex = i;
            Active.table = table;
        }
        else
            activeIndex = i;

        if (i < 0)
            i = Len - 1;

        activeIndex = i;
    }
}