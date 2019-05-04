using System;
using System.Linq;
using System.Threading.Tasks;
using ManagersApi.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PodcastManager;
using PodcastManager.Enums;
using PodcastManager.Models.NaoOuvo;
using SharpTestsEx;

namespace ManagersApi.Tests
{
    [TestClass]
    public class NaoOuvoTests
    {
        [TestMethod]
        public async Task ApiTest()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NaoOuvo);
            var episodeCollection = await currentManager.GetPodcastListAsync();

            var lastNaoOuvo = episodeCollection.First() as Episode;
            var timeDifference = DateTime.Now.Date - DateTime.Parse(lastNaoOuvo.PubDate).Date;
            var apiIsWorking = timeDifference < TimeSpan.FromDays(30);

            apiIsWorking.Should().Be(true);
        }

        [TestMethod]
        public async Task DownloadFail()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NaoOuvo);
            var success = await currentManager.DownloadPodcastAsync(null, null);

            success.Should().Be(false);
        }

        [TestMethod]
        public async Task DownloadSuccess()
        {
            var podcastManager = new Manager(new FakeFileHelper());
            var currentManager = podcastManager.GetManager(PodcastType.NaoOuvo);
            var episodeCollection = await currentManager.GetPodcastListAsync();
            
            var lastNaoOuvo = episodeCollection.First() as Episode;
            var success = await currentManager
                .DownloadPodcastAsync(lastNaoOuvo, "FakeDirectory");
            
            success.Should().Be(true);
        }
    }
}