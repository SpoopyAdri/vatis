namespace Vatsim.Vatis.Events;

public class KillRequestReceivedEventArgs
{
    public string Reason { get; set; }
    public KillRequestReceivedEventArgs(string reason)
    {
        Reason = reason;
    }
}