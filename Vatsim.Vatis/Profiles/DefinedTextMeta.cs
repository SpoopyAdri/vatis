using System;

namespace Vatsim.Vatis.Profiles;

[Serializable]
public class DefinedTextMeta
{
    public string Description { get; set; }
    public int Ordinal { get; set; }
    public string Text { get; set; }
    public bool Enabled { get; set; }
    public override string ToString()
    {
        return !string.IsNullOrEmpty(Description) ? Description : Text;
    }
}
