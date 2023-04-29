using Newtonsoft.Json;
using System.Collections.Generic;
using Vatsim.Vatis.Profiles.AtisFormat.Nodes.Converter;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes;
public class ObservationTime : BaseFormat
{
    public ObservationTime()
    {
        Template = new()
        {
            Text = "{time}Z",
            Voice = "{time} ZULU {special}"
        };
    }

    [JsonConverter(typeof(ObservationTimeConverter))]
    public List<int> StandardUpdateTime { get; set; }
}