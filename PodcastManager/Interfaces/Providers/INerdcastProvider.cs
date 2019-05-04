using PodcastManager.Models.NerdCast;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PodcastManager.Interfaces.Providers
{
    public interface INerdcastProvider
    {
        [Get("/wp-json/jovemnerd/v1/nerdcasts")]
        Task<List<Episode>> GetNerdCasts();

        [Get("/wp-json/jovemnerd/v1/nerdcasts/")]
        Task<List<Episode>> GetNerdCastsAlternative();
    }
}
