namespace Vatsim.Vatis.Events;

public class NotificationPosted : IEvent
{
    public string Message { get; set; }

    public NotificationPosted(string message)
    {
        Message = message;
    }
}
