using System;

namespace Vatsim.Vatis.Events;

public class MetarResponseReceivedEventArgs : EventArgs
{
    public string Metar { get; set; }
    public bool IsUpdated { get; set; }
    public MetarResponseReceivedEventArgs(string metar, bool isUpdated)
    {
        Metar = metar;
        IsUpdated = isUpdated;
    }
}