using System.Net.Http;
using System.Text;

namespace PodcastManager.Helpers
{
    internal class JsonContent : StringContent
    {
        public JsonContent(string content) : base(content, Encoding.UTF8, "application/json")
        {
        }
    }
}