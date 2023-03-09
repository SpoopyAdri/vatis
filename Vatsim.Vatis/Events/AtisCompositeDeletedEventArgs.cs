using System;

namespace Vatsim.Vatis.Events;

public class AtisCompositeDeletedEventArgs : EventArgs
{
    public Guid Id { get; set; }
    public AtisCompositeDeletedEventArgs(Guid id)
    {
        Id = id;
    }
}