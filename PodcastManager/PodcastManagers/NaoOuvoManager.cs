using PodcastManager.Enums;
using PodcastManager.Helpers;
using PodcastManager.Interfaces;
using PodcastManager.Models.NaoOuvo;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PodcastManager.PodcastManagers
{
    internal class NaoOuvoManager : IManager
    {
        public PodcastType Type => PodcastType.NaoOuvo;
        private const string NAOOUVO_FEED = "http://feeds.feedburner.com/naoouvo/";
        private List<string> currentFilesDownloading = new List<string>();
        private readonly IFileHelper _fileHelper;
        private WebApiClient WebClient;

        public NaoOuvoManager(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;

            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            WebClient = new WebApiClient(new HttpClient(handler), _fileHelper);
        }

        public async Task<List<IPodcastEpisode>> GetPodcastListAsync()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                var xmlString = await WebClient.GetResponseStringAsync(NAOOUVO_FEED);
                var feedXmlDocument = XDocument.Parse(xmlString.Replace("itunes:","itunes").Replace("media:", "media"));

                var episodeList = feedXmlDocument.Descendants()
                    .Where(e=>e.Name.LocalName.Equals("item", StringComparison.OrdinalIgnoreCase));
                
                return episodeList.Select(e =>
                {
                    var reader = new StringReader(e.ToString());
                    var xmlSerializer = new XmlSerializer(typeof(Episode));
                    return (IPodcastEpisode)xmlSerializer.Deserialize(reader);
                    
                }).ToList();
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

                if (episode is Episode naoOuvoEpisode && _fileHelper.DirectoryExists(directoryOut))
                {
                    var archiveName =  naoOuvoEpisode.Url.Substring(naoOuvoEpisode.Url.LastIndexOf("/") + 1);

                    if (currentFilesDownloading.Any(name => name.Equals(archiveName, StringComparison.CurrentCultureIgnoreCase)))
                        return false;

                    var httpclient = new HttpClient()
                    {
                        Timeout = (new TimeSpan(5, 0, 0))
                    };

                    var client = new WebApiClient(httpclient, _fileHelper);

                    currentFilesDownloading.Add(archiveName);
                    directoryOut = directoryOut.EndsWith("\\") ? directoryOut : directoryOut + "\\";

                    if (downloadInsertions)
                        await DownloadNaoOuvoInsertions(naoOuvoEpisode, directoryOut, archiveName);

                    var filePath = string.Concat(directoryOut, archiveName);

                    await client.GetAsync(naoOuvoEpisode.Url, filePath);

                    currentFilesDownloading.Remove(archiveName);
                    completedDownloadEvent?.Invoke(null, null);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error on NaoOuvo download.\n\nDetails{ex.Message}");
                return false;
            }

            return false;
        }

        private async Task DownloadNaoOuvoInsertions(
            Episode naoOuvoEpisode, string directoryOut, string archiveName)
        {
            var naoOuvoDirectory = string.Concat(
                directoryOut,
                Path.GetFileNameWithoutExtension(archiveName),
                "\\");

            if (!_fileHelper.DirectoryExists(naoOuvoDirectory))
                _fileHelper.CreateDirectory(naoOuvoDirectory);

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(naoOuvoEpisode.ImageObject.Href), $"{naoOuvoDirectory}Cover.jpg");
            }
        }
    }
}