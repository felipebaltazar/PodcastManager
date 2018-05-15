namespace PodcastManager.Interfaces
{
    public interface IPodcastEpisode
    {
        string Title { get; set; }
        string Image { get; set; }
        string Description { get; set; }
        string Url { get; set; }
    }
}