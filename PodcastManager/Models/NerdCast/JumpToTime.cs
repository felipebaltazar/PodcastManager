using Newtonsoft.Json;

namespace PodcastManager.Models.NerdCast
{
    public class JumpToTime
    {
        [JsonProperty("test")]
        public string TimeString { get; set; }

        [JsonProperty("start-time")]
        public int StartTime { get; set; }

        [JsonProperty("end-time")]
        public int EndTime { get; set; }
    }
}