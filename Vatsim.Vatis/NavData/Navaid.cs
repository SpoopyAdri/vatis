using Newtonsoft.Json;

namespace Vatsim.Vatis.NavData;

public class Navaid
{
    [JsonProperty("ID")]
    public string ID { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }
}
