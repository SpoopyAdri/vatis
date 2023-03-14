using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.Network;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Config;

public class AtisComposite : IAtisComposite
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Identifier { get; set; }
    public AtisType AtisType { get; set; }
    public List<ContractionMeta> Contractions { get; set; } = new List<ContractionMeta>();
    public int AtisFrequency { get; set; } = 18000;
    public ObservationTimeMeta ObservationTime { get; set; } = new ObservationTimeMeta();
    public MagneticVariationMeta MagneticVariation { get; set; } = new MagneticVariationMeta();
    public AtisVoiceMeta AtisVoice { get; set; } = new AtisVoiceMeta();
    public string IDSEndpoint { get; set; }
    public List<AtisPreset> Presets { get; set; } = new List<AtisPreset>();
    public List<DefinedText> AirportConditionDefinitions { get; set; } = new List<DefinedText>();
    public bool AirportConditionsBeforeFreeText { get; set; }
    public List<DefinedText> NotamDefinitions { get; set; } = new List<DefinedText>();
    public bool NotamsBeforeFreeText { get; set; }
    public List<TransitionLevel> TransitionLevels { get; set; } = new List<TransitionLevel>();
    public bool UseFaaFormat { get; set; } = true;
    public bool UseExternalAtisGenerator { get; set; }
    public bool UseNotamPrefix { get; set; } = true;
    public bool UseTransitionLevelPrefix { get; set; } = true;
    public bool UseMetricUnits { get; set; }
    public bool UseSurfaceWindPrefix { get; set; }
    public bool UseVisibilitySuffix { get; set; }
    public bool UseDecimalTerminology { get; set; }
    public bool UseTemperaturePlusPrefix { get; set; }

    internal AtisComposite Clone()
    {
        return new AtisComposite
        {
            Contractions = Contractions,
            AtisFrequency = AtisFrequency,
            ObservationTime = ObservationTime,
            MagneticVariation = MagneticVariation,
            Presets = Presets,
            AirportConditionDefinitions = AirportConditionDefinitions,
            AirportConditionsBeforeFreeText = AirportConditionsBeforeFreeText,
            NotamDefinitions = NotamDefinitions,
            NotamsBeforeFreeText = NotamsBeforeFreeText,
            TransitionLevels = TransitionLevels,
            UseFaaFormat = UseFaaFormat,
            UseExternalAtisGenerator = UseExternalAtisGenerator,
            UseNotamPrefix = UseNotamPrefix,
            UseTransitionLevelPrefix = UseTransitionLevelPrefix,
            UseMetricUnits = UseMetricUnits,
            UseSurfaceWindPrefix = UseSurfaceWindPrefix,
            UseVisibilitySuffix = UseVisibilitySuffix,
            UseDecimalTerminology = UseDecimalTerminology,
            UseTemperaturePlusPrefix = UseTemperaturePlusPrefix,
            AtisVoice = AtisVoice,
            IDSEndpoint = IDSEndpoint
        };
    }

    public override string ToString() => AtisType != AtisType.Combined ? $"{Name} ({Identifier}) {AtisType}" : $"{Name} ({Identifier})";

    [JsonIgnore] public Metar DecodedMetar { get; set; }
    [JsonIgnore] public AtisPreset CurrentPreset { get; set; }
    [JsonIgnore] public string CurrentAtisLetter { get; set; }
    [JsonIgnore] public string AcarsText { get; set; }
    [JsonIgnore] public uint AfvFrequency => ((uint)AtisFrequency + 100000) * 1000;
    [JsonIgnore] public string AtisCallsign { get; set; }
    [JsonIgnore] public MemoryStream MemoryStream { get; set; }
    [JsonIgnore] public Connection Connection { get; set; }

    [JsonIgnore] public EventHandler<ClientEventArgs<string>> MetarReceived;
    [JsonIgnore] public EventHandler<EventArgs> NewAtisUpdate;
}

[Serializable]
public class AtisVoiceMeta
{
    public bool UseTextToSpeech { get; set; } = true;
    public string Voice { get; set; } = "Default";
    [JsonIgnore]
    public string GetVoiceNameForRequest
    {
        get
        {
            switch (Voice)
            {
                case "Default":
                    return "default";
                case "US Male":
                    return "us-male";
                case "US Female":
                    return "us-female";
                case "UK Male":
                    return "uk-male";
                case "UK Female":
                    return "uk-female";
                default:
                    return "default";
            }
        }
    }
}

[Serializable]
public class ContractionMeta
{
    public string String { get; set; }
    public string Spoken { get; set; }
}

[Serializable]
public class MagneticVariationMeta
{
    public bool Enabled { get; set; } = false;
    public int MagneticDegrees { get; set; } = 0;
}

[Serializable]
public class ObservationTimeMeta
{
    public bool Enabled { get; set; } = false;
    public uint Time { get; set; } = 0;
}

[Serializable]
public class DefinedText
{
    public int Ordinal { get; set; }
    public string Text { get; set; }
    public bool Enabled { get; set; }
}

[Serializable]
public class TransitionLevel
{
    public int Low { get; set; }
    public int High { get; set; }
    public int Altitude { get; set; }
}