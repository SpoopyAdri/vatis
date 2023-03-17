namespace Vatsim.Vatis.Events;
public class AtisTemplateChanged : IEvent
{
    public string Value { get; set; }
    public AtisTemplateChanged(string value)
    {
        Value = value;
    }
}