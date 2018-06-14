using PodcastManager.Enums;
using PodcastManager.Helpers;
using PodcastManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("ManagersApi.Tests")]
namespace PodcastManager
{
    public class Manager
    {
        private readonly IEnumerable<IManager> _managers;
        private readonly IFileHelper _fileHelper;

        public Manager()
        {
            var interfaceType = typeof(IManager);

            _fileHelper = new FileHelper();
            _managers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x, _fileHelper) as IManager);
        }

        internal Manager(IFileHelper fileHelper)
        {
            var interfaceType = typeof(IManager);

            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
            _managers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x, _fileHelper) as IManager);
        }

        public IManager GetManager(PodcastType podcastType) =>
            _managers?.FirstOrDefault(a => a.Type == podcastType);
    }
}