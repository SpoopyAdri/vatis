using System;

namespace Vatsim.Vatis.Atis;
public class AtisBuilderException : Exception
{
    public AtisBuilderException(string message) : base(message)
    {
    }
}
