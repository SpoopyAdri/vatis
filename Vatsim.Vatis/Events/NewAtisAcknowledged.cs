using Vatsim.Vatis.Profiles;

namespace Vatsim.Vatis.Events;
public class NewAtisAcknowledged : IEvent
{
    public Composite Composite { get; set; }
    public NewAtisAcknowledged(Composite composite)
    {
        Composite = composite;
    }
}
