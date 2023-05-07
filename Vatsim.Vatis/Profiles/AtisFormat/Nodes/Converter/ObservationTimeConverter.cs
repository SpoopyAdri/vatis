using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vatsim.Vatis.Profiles.AtisFormat.Nodes.Converter;
public class ObservationTimeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(int);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        if (token.Type == JTokenType.Integer)
        {
            return new List<int>() { token.Value<int>() };
        }

        return token.ToObject<List<int>>();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
