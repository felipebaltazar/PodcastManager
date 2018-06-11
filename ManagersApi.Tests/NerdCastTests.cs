using System;
using System.Linq;
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
        public void ApiTest()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NerdCast);
            var episodeCollection = currentManager.GetPodcastListAsync().Result;
            var firstItem = episodeCollection.First();

            (firstItem is Episode).Should().Be(true);

            var lastNerdcast = firstItem as Episode;
            var timeDifference = DateTime.Now.Date - lastNerdcast.PubDate.Date;
            var apiIsWorking = timeDifference < TimeSpan.FromDays(7);

            apiIsWorking.Should().Be(true);
        }

        public void DownloadFail()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NerdCast);
            var success = currentManager.DownloadPodcastAsync(null,null);

            success.Should().Be(false);
        }
    }
}