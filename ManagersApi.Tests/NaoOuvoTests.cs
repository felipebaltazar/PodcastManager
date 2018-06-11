using System;
using System.Linq;
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
        public void ApiTest()
        {
            var podcastManager = new Manager();
            var currentManager = podcastManager.GetManager(PodcastType.NaoOuvo);
            var episodeCollection = currentManager.GetPodcastListAsync().Result;

            var lastNaoOuvo = episodeCollection.First() as Episode;
            var timeDifference = DateTime.Now.Date - DateTime.Parse(lastNaoOuvo.PubDate).Date;
            var apiIsWorking = timeDifference < TimeSpan.FromDays(7);

            apiIsWorking.Should().Be(true);
        }
    }
}