using System;
using System.IO;

namespace Vatsim.Vatis.Events;

public class RecordedAtisChangedEventArgs : EventArgs
{
    public MemoryStream AtisMemoryStream { get; set; }
    public RecordedAtisChangedEventArgs(MemoryStream stream)
    {
        AtisMemoryStream = stream;
    }
}