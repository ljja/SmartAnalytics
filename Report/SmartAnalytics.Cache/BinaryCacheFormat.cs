using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SmartAnalytics.Cache
{
    public class BinaryCacheFormat : ICacheFormat
    {
        public string Serialize<T>(T t)
        {
            var byteArray = BinarySerialize(t);

            return Encoding.ASCII.GetString(byteArray);
        }
        
        public T Deserialize<T>(string text)
        {
            var byteArray = Encoding.ASCII.GetBytes(text);

            return BinaryDeserialize<T>(byteArray);
        }


        private byte[] BinarySerialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                var objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        private T BinaryDeserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(stream))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}
