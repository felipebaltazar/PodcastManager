using PodcastManager.Enums;
using PodcastManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PodcastManager
{
    public class Manager
    {
        private readonly IEnumerable<IManager> _managers;

        public Manager()
        {
            var interfaceType = typeof(IManager);

            _managers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IManager);
        }

        public IManager GetManager(PodcastType podcastType) =>
            _managers?.FirstOrDefault(a => a.Type == podcastType);
    }
}