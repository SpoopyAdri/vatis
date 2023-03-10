using System;

namespace Vatsim.Vatis.Events;

public class MetarResponseReceived : EventArgs
{
    public string Metar { get; set; }
    public bool IsUpdated { get; set; }
    public MetarResponseReceived(string metar, bool isUpdated)
    {
        Metar = metar;
        IsUpdated = isUpdated;
    }
}