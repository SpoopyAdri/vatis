using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes.Converter;
public class CloudConverter : JsonConverter<Dictionary<string, object>>
{
    public override Dictionary<string, object> ReadJson(JsonReader reader, Type objectType, Dictionary<string, object> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var result = serializer.Deserialize<Dictionary<string, object>>(reader);

        if (result != null)
        {
            foreach (var kv in result)
            {
                if (kv.Value.GetType() == typeof(string))
                {
                    result[kv.Key] = kv.Key switch
                    {
                        "FEW" => new CloudType("FEW{altitude}", kv.Value.ToString()),
                        "SCT" => new CloudType("SCT{altitude}{convective}", kv.Value.ToString()),
                        "BKN" => new CloudType("BKN{altitude}{convective}", kv.Value.ToString()),
                        "OVC" => new CloudType("OVC{altitude}{convective}", kv.Value.ToString()),
                        "VV" => new CloudType("VV{altitude}", kv.Value.ToString()),
                        "NSC" => new CloudType("NSC", kv.Value.ToString()),
                        "NCD" => new CloudType("NCD", kv.Value.ToString()),
                        "CLR" => new CloudType("CLR", kv.Value.ToString()),
                        "SKC" => new CloudType("SKC", kv.Value.ToString()),
                        _ => new CloudType(kv.Key, kv.Value.ToString()),
                    };
                }
            }
        }

        return result;
    }

    public override void WriteJson(JsonWriter writer, Dictionary<string, object> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
