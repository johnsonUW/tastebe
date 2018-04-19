using System;
using System.IO;
using System.Xml.Serialization;
using Framework.ExceptionHandling;

namespace Framework.Utils
{
    public static class XmlSerializationExtension
    {
        public static string ToXml<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                using (var stringWriter = new StringWriter())
                {
                    xmlserializer.Serialize(stringWriter, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception)
            {
                throw new ApiException(ApiErrorCode.SerializationError);
            }
        }

        public static T FromXml<T>(this string value)
        {
            if (value == null)
            {
                return default(T);
            }
            try
            {
                var serilizer = new XmlSerializer(typeof(T));
                using (var reader = new StringReader(value))
                {
                    return (T)serilizer.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                throw new ApiException(ApiErrorCode.SerializationError);
            }
        }
    }
}