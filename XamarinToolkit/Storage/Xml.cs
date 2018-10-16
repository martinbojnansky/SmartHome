using System.IO;
using System.Text;
using System.Xml.Serialization;
using XamarinToolkit.IoC;

namespace XamarinToolkit.Storage
{
    public class Xml : IResolvable
    {
        public T FromXml<T>(string json)
        {
            var serializer = GetSerializer<T>();

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)serializer.Deserialize(stream);
            }
        }

        public string ToXml<T>(T obj)
        {
            var serializer = GetSerializer<T>();

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, obj);
                var jsonArray = ms.ToArray();
                return Encoding.UTF8.GetString(jsonArray, 0, jsonArray.Length);
            }
        }

        private XmlSerializer GetSerializer<T>()
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer;
        }
    }
}
