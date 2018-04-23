using PodcastManager.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;

namespace PodcastManager.Interfaces
{
    public interface IManager
    {
        PodcastType Type { get; }

        Task<List<IPodcastEpisode>> GetPodcastListAsync();

        Task<bool> DownloadPodcastAsync(
            IPodcastEpisode episode, string directoryOut, AsyncCompletedEventHandler completedDownloadEvent = null,
            DownloadProgressChangedEventHandler downloadProgressChangedEvent = null, bool downloadInsertions = false);
    }
}