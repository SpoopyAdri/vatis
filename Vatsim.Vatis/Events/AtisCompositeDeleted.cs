using System;

namespace Vatsim.Vatis.Events;

public class AtisCompositeDeleted : IEvent
{
    public Guid Id { get; set; }
    public AtisCompositeDeleted(Guid id)
    {
        Id = id;
    }
}