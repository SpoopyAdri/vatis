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
    List<DefinedText> AirportConditionDefinitions { get; set; }
    bool AirportConditionsBeforeFreeText { get; set; }
    List<DefinedText> NotamDefinitions { get; set; }
    bool NotamsBeforeFreeText { get; set; }
    List<TransitionLevel> TransitionLevels { get; set; }
    int AtisFrequency { get; set; }
    ObservationTimeMeta ObservationTime { get; set; }
    MagneticVariationMeta MagneticVariation { get; set; }
    AtisVoiceMeta AtisVoice { get; set; }
    string IDSEndpoint { get; set; }
    List<Preset> Presets { get; set; }
    Preset CurrentPreset { get; set; }
    string CurrentAtisLetter { get; set; }
    MemoryStream MemoryStream { get; set; }
    CodeRange CodeRange { get; set; }
    bool UseFaaFormat { get; set; }
    bool UseNotamPrefix { get; set; }
    bool UseTransitionLevelPrefix { get; set; }
    bool UseMetricUnits { get; set; }
    bool UseSurfaceWindPrefix { get; set; }
    bool UseVisibilitySuffix { get; set; }
    bool UseDecimalTerminology { get; set; }
    bool UseTemperaturePlusPrefix { get; set; }
    bool UseZuluTimeSuffix { get; set; }
}