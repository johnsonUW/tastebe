using System;
using Framework.ExceptionHandling;
using Newtonsoft.Json;

namespace Framework.Utils
{
    public static class JsonSerializationExtension
    {
        public static string ToJson<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var str = JsonConvert.SerializeObject(value);
                return str;
            }
            catch (Exception)
            {
                throw new ApiException(ApiErrorCode.SerializationError);
            }
        }

        public static T FromJson<T>(this string value)
        {
            if (value == null)
            {
                return default(T);
            }
            try
            {
                var obj = JsonConvert.DeserializeObject<T>(value);
                return obj;
            }
            catch (Exception)
            {
                throw new ApiException(ApiErrorCode.SerializationError);
            }
        }
    }
}