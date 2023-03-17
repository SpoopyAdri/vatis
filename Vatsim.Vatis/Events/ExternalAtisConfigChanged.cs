namespace Vatsim.Vatis.Events;

public class ExternalAtisConfigChanged : IEvent
{
    public ExternalAtisComponent Component { get; set; }
    public string Value { get; set; }

    public ExternalAtisConfigChanged(ExternalAtisComponent component, string value)
    {
        Component = component;
        Value = value;
    }
}

public enum ExternalAtisComponent
{
    Url,
    ArrivalRunways,
    DepartureRunways,
    Approaches,
    Remarks
}
