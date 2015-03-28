using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Taut.Test
{
    public class JsonLoader
    {
        public static IEnumerable<T> LoadJsonCollection<T>(string filePath)
        {
            return ParseJsonArrayTo<T>(ReadFile(filePath));
        }

        public static T LoadJson<T>(string filePath)
        {
            return ParseJsonTo<T>(ReadFile(filePath));
        }

        private static string ReadFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        private static IEnumerable<T> ParseJsonArrayTo<T>(string json)
        {
            var settings = SerializerSettings;
            var results = JsonConvert.DeserializeObject<T[]>(json, settings);
            return results.ToList<T>();
        }

        private static T ParseJsonTo<T>(string json)
        {
            var settings = SerializerSettings;
            var result = JsonConvert.DeserializeObject<T>(json, settings);
            return result;
        }

        private static JsonSerializerSettings _serializerSettings;
        private static JsonSerializerSettings SerializerSettings
        {
            get
            {
                return _serializerSettings ?? (_serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
            }
        }
    }
}
