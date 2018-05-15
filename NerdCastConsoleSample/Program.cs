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
            var podcastTypes = Enum.GetValues(typeof(PodcastType));

            foreach (PodcastType podcastType in podcastTypes)
            {
                var currentManager = podcastManager.GetManager(podcastType);
                var episodeCollection = currentManager.GetPodcastListAsync().Result;

                Console.WriteLine($"{episodeCollection.Count} {podcastType.ToString()}'s founds...\n");

                if(episodeCollection.Count > 0)
                    Console.WriteLine("First item:\n" +
                                      $"\tTitle: {episodeCollection[0].Title}\n" +
                                      $"\tUrl {episodeCollection[0].Url}\n" +
                                      $"\tImage {episodeCollection[0].Image}\n");

            }

            Console.ReadKey();
        }
    }
}