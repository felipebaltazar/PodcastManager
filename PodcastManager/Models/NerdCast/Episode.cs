using Newtonsoft.Json;
using PodcastManager.Interfaces;
using System;
using System.Collections.Generic;

namespace PodcastManager.Models.NerdCast
{
    public class Episode : IPodcastEpisode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("pub_date")]
        public DateTime PubDate { get; set; }

        [JsonProperty("modified_at")]
        public DateTime ModifiedAt { get; set; }
        
        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("title")]
        public string Title {get; set;}

        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        [JsonProperty("episode")]
        public string EpisodeNumber { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("friendly_post_type")]
        public string FriendlyPostType { get; set; }

        [JsonProperty("friendly_post_type_slug")]
        public string FriendlyPostTypeSlug { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("image_alt")]
        public string ImageAlt { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnail Thumbnails { get; set; }

        [JsonProperty("audio_low")]
        public string AudioLow { get; set; }

        [JsonProperty("audio_medium")]
        public string AudioMedium { get; set; }

        [JsonProperty("audio_high")]
        public string AudioHigh { get; set; }

        [JsonProperty("audio_zip")]
        public string AudioZip { get; set; }

        [JsonProperty("insertions")]
        public List<Insertion> Insertions { get; set; }

        [JsonProperty("description")]
        public string Description {get; set;}
        
        [JsonProperty("jump-to-time")]
        public JumpToTime JumpToTime { get; set; }

        [JsonProperty("guests")]
        public string Guests { get; set; }

        [JsonProperty("post_type_class")]
        public string PostTypeClass { get; set; }

        [JsonProperty("product_obj")]
        public ProductObject ProductObj { get; set; }
    }
}