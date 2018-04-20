using Newtonsoft.Json;

namespace PodcastManager.Models.NerdCast
{
    public class Insertion
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("button-title")]
        public string ButtonTitle{ get; set; }

        [JsonProperty("start-time")]
        public int StartTime{ get; set; }

        [JsonProperty("end-time")]
        public int EndTime { get; set; }

        [JsonProperty("sound")]
        public bool Sound { get; set; }
    }
}