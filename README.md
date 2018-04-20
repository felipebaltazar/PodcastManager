# PodcastManager
Tool to download and organize your Podcasts programmatically

## Current Supported Podcasts

The banks below were added in the order they are listed


| Name                                                                                                                                                                                          | EpisodeList | Additional Info                                                                                       | Method                               | Status |
| ---                                                                                                                                                                                           | ---     | ---                                                                                                       | ---                                  | ---    |
| [![Nerdcast](https://jovemnerd.com.br/wp-content/uploads/2016/07/logo-jn.png)]                              | Yes                 | Episode Number, Title, Published Date, Insertions         | Reversed Web API                  | OK     |


## Nuget Install
<pre><code>
Install-Package PodcastManager
</code></pre>

## Usage
<pre><code>
			var podcastManager = new Manager();
            var nerdCast = podcastManager.GetManager(PodcastType.NerdCast);
            
            var podcastCollection = await nerdCast.GetPodcastListAsync();
</code></pre>