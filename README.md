# PodcastManager
Tool to download and organize your Podcasts programmatically

## Current Supported Podcasts

The banks below were added in the order they are listed


| Name                                                                                                                                                                                          | EpisodeList | Additional Info                                                                                       | Method                               | Status |
| ---                                                                                                                                                                                           | ---     | ---                                                                                                       | ---                                  | ---    |
| [![Nerdcast](https://github.com/felipebaltazar/PodcastManager/blob/master/PodcastManager/Logos/NerdCast.png)](https://github.com/felipebaltazar/PodcastManager/blob/master/PodcastManager/PodcastManagers/NerdCastManager.cs)                              | Yes                 | Episode Number, Title, Published Date, Insertions         | Reversed Web API                  | OK     |


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