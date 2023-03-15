using Newtonsoft.Json;

namespace Vatsim.Vatis.NavData;

public class Airport
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Lat")]
    public double Latitude { get; set; }

    [JsonProperty("Lon")]
    public double Longitude { get; set; }
}