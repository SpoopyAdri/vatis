using Vatsim.Vatis.Config;

namespace Vatsim.Vatis.Events;
public class NewAtisAcknowledged : IEvent
{
    public AtisComposite Composite { get; set; }
    public NewAtisAcknowledged(AtisComposite composite)
    {
        Composite = composite;
    }
}
