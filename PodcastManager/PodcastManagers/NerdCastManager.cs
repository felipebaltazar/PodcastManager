using PodcastManager.Enums;
using PodcastManager.Helpers;
using PodcastManager.Interfaces;
using PodcastManager.Models.NerdCast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PodcastManager.PodcastManagers
{
    internal class NerdCastManager : IManager
    {
        private const string NERDCAST_API = "https://api.jovemnerd.com.br/wp-json/jovemnerd/v1/nerdcasts";
        private const string NERDCAST_DOWNLOAD_URL = "https://nerdcast-cdn.jovemnerd.com.br/";
        private List<string> currentFilesDownloading = new List<string>();
        private readonly IFileHelper _fileHelper;
        private readonly WebClient _webClient;

        public PodcastType Type => PodcastType.NerdCast;

        public NerdCastManager(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
            _webClient = new WebClient();
        }
        
        public async Task<List<IPodcastEpisode>> GetPodcastListAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var episodeList = await Json.DeserializeObjectAsync<List<Episode>>(
                    _webClient.DownloadString(NERDCAST_API));

                var episodeListAlternative = await Json.DeserializeObjectAsync<List<Episode>>(
                    _webClient.DownloadString(string.Concat(NERDCAST_API, "/")));

                return episodeList[0]?.PublishedAt > episodeListAlternative[0]?.PublishedAt
                    ? episodeList.Select(e=> e as IPodcastEpisode).ToList()
                    : episodeListAlternative.Select(e => e as IPodcastEpisode).ToList();
            }

            return null;
        }

        public async Task<bool> DownloadPodcastAsync(
            IPodcastEpisode episode, string directoryOut, AsyncCompletedEventHandler completedDownloadEvent = null,
            DownloadProgressChangedEventHandler downloadProgressChangedEvent = null, bool downloadInsertions = false)
        {
            try
            {
                if (string.IsNullOrEmpty(directoryOut))
                    return false;

                if (!_fileHelper.DirectoryExists(directoryOut))
                    _fileHelper.CreateDirectory(directoryOut);

                if (episode is Episode nerdCastEpisode && _fileHelper.DirectoryExists(directoryOut))
                {
                    var archiveName =
                        nerdCastEpisode.AudioHigh?.Substring(nerdCastEpisode.AudioHigh.LastIndexOf("/") + 1) ??
                        nerdCastEpisode.AudioMedium?.Substring(nerdCastEpisode.AudioMedium.LastIndexOf("/") + 1) ??
                        nerdCastEpisode.AudioLow.Substring(nerdCastEpisode.AudioLow.LastIndexOf("/") + 1);

                    if (currentFilesDownloading.Any(name => name.Equals(archiveName, StringComparison.CurrentCultureIgnoreCase)))
                        return false;

                    var httpclient = new HttpClient()
                    {
                        BaseAddress = new Uri(NERDCAST_DOWNLOAD_URL),
                        Timeout = (new TimeSpan(5, 0, 0))
                    };

                    var client = new WebApiClient(httpclient, _fileHelper);

                    currentFilesDownloading.Add(archiveName);
                    directoryOut = directoryOut.EndsWith("\\") ? directoryOut : directoryOut + "\\";

                    if (downloadInsertions)
                        await DownloadNerdCastInsertions(nerdCastEpisode, directoryOut, archiveName);

                    var filePath = string.Concat(directoryOut, archiveName);

                    await client.GetAsync(archiveName, filePath);

                    currentFilesDownloading.Remove(archiveName);
                    completedDownloadEvent?.Invoke(null, null);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error on NerdCast download.\n\nDetails{ex.Message}");
                return false;
            }

            return false;
        }

        public async Task DownloadNerdCastInsertions(
            Episode episodio, string directoryOut, string archiveName)
        {
            var nerdCastDirectory = string.Concat(
                directoryOut,
                Path.GetFileNameWithoutExtension(archiveName),
                "\\");

            if (!_fileHelper.DirectoryExists(nerdCastDirectory))
                _fileHelper.CreateDirectory(nerdCastDirectory);

            await _webClient.DownloadFileTaskAsync(new Uri(episodio.Image), $"{nerdCastDirectory}Cover.jpg");

            foreach (var image in episodio.Insertions)
            {
                if (image?.Image?.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    await _webClient.DownloadFileTaskAsync(
                        new Uri(image.Image),
                        $"{nerdCastDirectory}{image.Image.Substring(image.Image.LastIndexOf("/"))}");
                }
            }

            await GenereteInsertionsJson(episodio, nerdCastDirectory, archiveName);
        }

        private async Task GenereteInsertionsJson(
            Episode episodio, string fullArchiveDirectory, string archiveName)
        {
            var info = await Json.SerializeObjectAsync(episodio.JumpToTime);
            var jsonEpisodeArts = await Json.SerializeObjectAsync(
                episodio.Insertions.Where(a => a.Image.StartsWith("https", StringComparison.OrdinalIgnoreCase)));

            _fileHelper.WriteAllText($"{fullArchiveDirectory}{Path.GetFileNameWithoutExtension(archiveName)}.info", info);
            _fileHelper.WriteAllText($"{fullArchiveDirectory}{Path.GetFileNameWithoutExtension(archiveName)}.json", jsonEpisodeArts);
        }
    }
}