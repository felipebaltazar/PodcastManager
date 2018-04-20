using Newtonsoft.Json;

namespace PodcastManager.Models.NerdCast
{
    public class ProductObject
    {
        [JsonProperty("term_id")]
        public int TermId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("term_group")]
        public int TermGroup { get; set; }

        [JsonProperty("term_taxonomy_id")]
        public int TermTaxonomyId { get; set; }

        [JsonProperty("taxonomy")]
        public string Taxonomy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parent")]
        public int Parent { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }
    }
}