using System;

namespace Vatsim.Vatis.Profiles;

[Serializable]
public class AtisVoiceMeta
{
    public bool UseTextToSpeech { get; set; } = true;
    public string Voice { get; set; } = "Default";
}
