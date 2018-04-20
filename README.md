# PodcastManager
Tool to download and organize your Podcasts programmatically

[![Build Status](https://travis-ci.org/felipebaltazar/PodcastManager.svg?branch=master)](https://travis-ci.org/felipebaltazar/PodcastManager)

## Current Supported Podcasts

The banks below were added in the order they are listed


| Name                                                                                                                                                                                          | EpisodeList | Additional Info                                                                                       | Method                               | Status |
| ---                                                                                                                                                                                           | ---     | ---                                                                                                       | ---                                  | ---    |
| [![Nerdcast](https://github.com/felipebaltazar/PodcastManager/blob/master/Logos/Nerdcast.png)](https://github.com/felipebaltazar/PodcastManager/blob/master/PodcastManager/PodcastManagers/NerdCastManager.cs)                              | Yes                 | Episode Number, Title, Published Date, Insertions         | Reversed Web API                  | OK     |


## Nuget Install
```
Install-Package PodcastManager
```

## Usage
```csharp
	    var podcastManager = new Manager();
            var nerdCast = podcastManager.GetManager(PodcastType.NerdCast);
            
            var podcastCollection = await nerdCast.GetPodcastListAsync();
```
