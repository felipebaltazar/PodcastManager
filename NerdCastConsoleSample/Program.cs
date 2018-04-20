using PodcastManager;
using PodcastManager.Enums;
using System;

namespace NerdCastConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var podcastManager = new Manager();
            var nerdCast = podcastManager.GetManager(PodcastType.NerdCast);
            
            var podcastCollection = nerdCast.GetPodcastListAsync().Result;
            Console.ReadKey();
        }
    }
}