using System;
using System.Linq;
using System.Threading.Tasks;
using ManagersApi.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PodcastManager;
using PodcastManager.Enums;
using PodcastManager.Models.NerdCast;
using SharpTestsEx;

namespace ManagersApi.Tests
{
    [TestClass]
    public class NerdCastTests
    {
        [TestMethod]
        public async Task ApiTest()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NerdCast);
            var episodeCollection = await currentManager.GetPodcastListAsync();
            var firstItem = episodeCollection.First();

            (firstItem is Episode).Should().Be(true);

            var lastNerdcast = firstItem as Episode;
            var timeDifference = DateTime.Now.Date - lastNerdcast.PubDate.Date;
            var apiIsWorking = timeDifference < TimeSpan.FromDays(7);

            apiIsWorking.Should().Be(true);
        }

        [TestMethod]
        public async Task DownloadFail()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NerdCast);
            var success = await currentManager.DownloadPodcastAsync(null,null);

            success.Should().Be(false);
        }

        [TestMethod]
        public async Task DownloadSuccess()
        {
            var podcastManager = new Manager(new FakeFileHelper());
            var currentManager = podcastManager.GetManager(PodcastType.NerdCast);
            var episodeCollection = await currentManager.GetPodcastListAsync();
            
            var lastNerdcast = episodeCollection.First() as Episode;
            var success = await currentManager
                .DownloadPodcastAsync(lastNerdcast, "FakeDirectory");

            success.Should().Be(true);
        }
    }
}