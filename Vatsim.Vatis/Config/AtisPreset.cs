using Newtonsoft.Json;
using System;

namespace Vatsim.Vatis.Config;

[System.Serializable]
public class AtisPreset : IAtisProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string AirportConditions { get; set; }
    public string Notams { get; set; }
    public string ArbitraryText { get; set; }
    public string Template { get; set; }
    public ExternalGenerator ExternalGenerator { get; set; } = new();

    [JsonIgnore] public bool IsAirportConditionsDirty { get; set; }
    [JsonIgnore] public bool IsNotamsDirty { get; set; }

    internal AtisPreset Clone()
    {
        return new AtisPreset
        {
            Id = Guid.NewGuid(),
            Name = Name,
            AirportConditions = AirportConditions,
            Notams = Notams,
            ArbitraryText = ArbitraryText,
            Template = Template,
            ExternalGenerator = ExternalGenerator
        };
    }

    public override string ToString() => Name;
}

public class ExternalGenerator
{
    public bool Enabled { get; set; }
    public string Url { get; set; }
    public string Arrival { get; set; }
    public string Departure { get; set; }
    public string Approaches { get; set; }
    public string Remarks { get; set; }
}