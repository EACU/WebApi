using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EACA_API.Services
{
    public class Response<T>
    {
        public int Count { get; set; }
        public List<T> ListGroups { get; set; }
    }

    public static class Serializer
    {
        public static void Serialize<T>(this T arg, string fileName)
        {
            string res = JsonConvert.SerializeObject(arg, Formatting.Indented);
            File.WriteAllText(fileName, res);
        }

        public static T Deserialize<T>(string fileName)
        {
            string json = File.ReadAllText(fileName);
            T res = JsonConvert.DeserializeObject<T>(json);
            return res;
        }
    }
}
