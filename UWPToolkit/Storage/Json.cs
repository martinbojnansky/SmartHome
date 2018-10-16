using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using UWPToolkit.IoC;

namespace UWPToolkit.Storage
{
    public class Json : IResolvable
    {
        private DataContractJsonSerializerSettings Settings { get; set; } = new DataContractJsonSerializerSettings
        {
            DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture)
        };

        private DataContractJsonSerializer GetSerializer<T>()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), Settings);

            return serializer;
        }

        public T FromJson<T>(string json)
        {
            var serializer = GetSerializer<T>();

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        public string ToJson<T>(T obj)
        {
            var serializer = GetSerializer<T>();

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                var jsonArray = ms.ToArray();
                return Encoding.UTF8.GetString(jsonArray, 0, jsonArray.Length);
            }
        }
    }
}
