using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Vatsim.Vatis.Events;
using Vatsim.Vatis.NavData;
using Vatsim.Vatis.Network;
using Vatsim.Vatis.UI.Dialogs;
using Vatsim.Vatis.Weather.Objects;

namespace Vatsim.Vatis.Profiles;

public class Composite : IComposite, ICloneable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Identifier { get; set; }
    public AtisType AtisType { get; set; }
    public CodeRangeMeta CodeRange { get; set; } = new CodeRangeMeta('A', 'Z');
    public uint Frequency { get; set; } = 118000000;
    public AtisVoiceMeta AtisVoice { get; set; } = new AtisVoiceMeta();
    public string IDSEndpoint { get; set; }
    public bool UseNotamPrefix { get; set; } = true;
    public bool UseTransitionLevelPrefix { get; set; } = true;
    public bool UseDecimalTerminology { get; set; }
    public bool AirportConditionsBeforeFreeText { get; set; }
    public bool NotamsBeforeFreeText { get; set; }
    public MagneticVariationMeta MagneticVariation { get; set; } = new MagneticVariationMeta();
    public List<Preset> Presets { get; set; } = new List<Preset>();
    public List<ContractionMeta> Contractions { get; set; } = new List<ContractionMeta>();
    public List<DefinedTextMeta> AirportConditionDefinitions { get; set; } = new List<DefinedTextMeta>();
    public List<DefinedTextMeta> NotamDefinitions { get; set; } = new List<DefinedTextMeta>();
    public List<TransitionLevelMeta> TransitionLevels { get; set; } = new List<TransitionLevelMeta>();
    public AtisFormat.AtisFormat AtisFormat { get; set; } = new();

    // Legacy 
    [JsonProperty] private int AtisFrequency { set => Frequency = (uint)((value + 100000) * 1000); }
    [JsonProperty] private dynamic ObservationTime { set => AtisFormat.ObservationTime.StandardUpdateTime = value.Time; }

    public object Clone()
    {
        var composite = (Composite)MemberwiseClone();
        composite.Id = Guid.NewGuid();
        return composite;
    }

    public override string ToString() => AtisType != AtisType.Combined ? $"{Name} ({Identifier}) {AtisType}" : $"{Name} ({Identifier})";

    [JsonIgnore] public Metar DecodedMetar { get; set; }
    [JsonIgnore] public Preset CurrentPreset { get; set; }
    [JsonIgnore] public string AtisLetter { get; set; }
    [JsonIgnore] public string TextAtis { get; set; }
    [JsonIgnore] public string AtisCallsign { get; set; }
    [JsonIgnore] public MemoryStream RecordedMemoryStream { get; set; }
    [JsonIgnore] public Connection Connection { get; set; }
    [JsonIgnore] public Airport AirportData { get; set; }
    [JsonIgnore] public bool IsFaaAtis => Identifier.StartsWith("K") || Identifier.StartsWith("P");

    [JsonIgnore] public EventHandler<ClientEventArgs<string>> MetarReceived;
    [JsonIgnore] public EventHandler<ClientEventArgs<string>> NewAtisUpdate;
    [JsonIgnore] public EventHandler DecodedMetarUpdated;
}