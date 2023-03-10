namespace Vatsim.Vatis.Events;

public class StartupStatusChanged : IEvent
{
    public string Status { get; set; }
    public StartupStatusChanged(string status)
    {
        Status = status;
    }
}