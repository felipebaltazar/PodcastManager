using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PodcastManager.Helpers
{
    internal class Json
    {
        public static Task<string> SerializeObjectAsync(object value) =>
            Task.Run(() => JsonConvert.SerializeObject(value));

        public static Task<T> DeserializeObjectAsync<T>(string json) =>
            Task.Run(() => JsonConvert.DeserializeObject<T>(json));
    }
}
