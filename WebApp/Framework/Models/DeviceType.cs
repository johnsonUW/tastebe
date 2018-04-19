using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Framework.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeviceType
    {
        iOS,
        Android
    }
}