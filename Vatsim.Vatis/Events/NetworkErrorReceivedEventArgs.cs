using System;

namespace Vatsim.Vatis.Events;

public class NetworkErrorReceivedEventArgs : EventArgs
{
    public string Error { get; set; }
    public NetworkErrorReceivedEventArgs(string error)
    {
        Error = error;
    }
}