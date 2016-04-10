using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SmartAnalytics.Cache
{
    public class JsonCacheFormat : ICacheFormat
    {
        public string DateTimeFormat = "yyyyMMdd";

        public string Serialize<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text, new IsoDateTimeConverter { DateTimeFormat = DateTimeFormat });
        }
    }
}
