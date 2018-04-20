using Newtonsoft.Json;

namespace PodcastManager.Models.NerdCast
{
    public class Thumbnail
    {
        [JsonProperty("img-1x1-128x128")]
        public string Img128x128 { get; set; }

        [JsonProperty("img-4x3-735x532")]
        public string Img735x532 { get; set; }

        [JsonProperty("img-4x3-355x266")]
        public string Img355x266 { get; set; }

        [JsonProperty("img-4x3-100x75")]
        public string Img100x75 { get; set; }

        [JsonProperty("img-16x9-1210x544")]
        public string Img1210x544 { get; set; }

        [JsonProperty("img-16x9-1440x500")]
        public string Img1440x500 { get; set; }

        [JsonProperty("img-16x9-760x428")]
        public string Img760x428 { get; set; }

        [JsonProperty("img-16x9-396x222")]
        public string Img396x222 { get; set; }

        [JsonProperty("img-16x10-726x444")]
        public string Img726x444 { get; set; }
    }
}