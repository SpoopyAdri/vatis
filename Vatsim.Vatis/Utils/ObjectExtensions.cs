using Newtonsoft.Json;

namespace Vatsim.Vatis.Utils;
public static class ObjectExtensions
{
    public static T Clone<T>(this T obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }
}