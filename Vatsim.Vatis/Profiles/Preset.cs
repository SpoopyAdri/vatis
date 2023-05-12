using System;
using System.Linq;
using Newtonsoft.Json;

namespace Vatsim.Vatis.Profiles;

[Serializable]
public class Preset : IPreset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string AirportConditions { get; set; }
    public string Notams { get; set; }
    public string Template { get; set; }
    public ExternalGenerator ExternalGenerator { get; set; } = new();

    [JsonIgnore] 
    public bool IsAirportConditionsDirty { get; set; }

    [JsonIgnore] 
    public bool IsNotamsDirty { get; set; }

    [JsonIgnore]
    public bool HasClosingVariable => new string[] { "[CLOSING]", "$CLOSING", "[CLOSING:VOX]", "$CLOSING:VOX" }.Any(n => Template.Contains(n));

    public override string ToString() => Name;
}