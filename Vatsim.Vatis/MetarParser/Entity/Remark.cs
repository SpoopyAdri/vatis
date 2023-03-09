using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vatsim.Vatis.MetarParser.Entity;

public sealed class Remark
{
    private readonly List<string> _remarks = new List<string>();

    public ReadOnlyCollection<string> Remarks
    {
        get
        {
            return new ReadOnlyCollection<string>(_remarks);
        }
    }

    public void AddRemark(string rmk)
    {
        _remarks.Add(rmk);
    }
}