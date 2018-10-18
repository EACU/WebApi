using EACA_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EACA_API.Controllers
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
