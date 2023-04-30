using System;
using System.Collections.Generic;
using System.IO;

namespace Vatsim.Vatis.Profiles;

public interface IComposite
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Identifier { get; set; }
    List<ContractionMeta> Contractions { get; set; }
    List<DefinedTextMeta> AirportConditionDefinitions { get; set; }
    bool AirportConditionsBeforeFreeText { get; set; }
    List<DefinedTextMeta> NotamDefinitions { get; set; }
    bool NotamsBeforeFreeText { get; set; }
    uint Frequency { get; set; }
    AtisVoiceMeta AtisVoice { get; set; }
    string IDSEndpoint { get; set; }
    List<Preset> Presets { get; set; }
    Preset CurrentPreset { get; set; }
    string AtisLetter { get; set; }
    MemoryStream RecordedMemoryStream { get; set; }
    CodeRangeMeta CodeRange { get; set; }
    AtisFormat.AtisFormat AtisFormat { get; set; }
    bool UseNotamPrefix { get; set; }
    bool UseDecimalTerminology { get; set; }
}