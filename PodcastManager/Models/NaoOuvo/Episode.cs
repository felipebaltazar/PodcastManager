using PodcastManager.Interfaces;
using System.Xml.Serialization;

namespace PodcastManager.Models.NaoOuvo
{
    [XmlRoot("item")]
    public class Episode : IPodcastEpisode
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("itunesauthor")]
        public string ItunesAuthor { get; set; }

        [XmlElement("itunessubtitle")]
        public string Subtitle { get; set; }

        [XmlElement("itunessummary")]
        public string Description { get; set; }

        [XmlElement("itunesimage")]
        public Image Image { get; set; }

        [XmlElement("enclosure")]
        public UrlContent Enclosure { get; set; }

        [XmlElement("guid")]
        public string Url { get; set; }

        [XmlElement("pubDate")]
        public string PubDate { get; set; }

        [XmlElement("itunesduration")]
        public string Duration { get; set; }

        [XmlElement("ituneskeywords")]
        public string Keywords { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("mediacontent")]
        public UrlContent MediaContent { get; set; }

        [XmlElement("itunesexplicit")]
        public string Explicit { get; set; }

    }
    public class Image
    {
        [XmlAttribute("href")]
        public string Href { get; set; }
    }

    public class UrlContent
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}