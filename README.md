# PodcastManager
Tool to download and organize your Podcasts programmatically

[![Build Status](https://travis-ci.org/felipebaltazar/PodcastManager.svg?branch=master)](https://travis-ci.org/felipebaltazar/PodcastManager)  [![Build status](https://ci.appveyor.com/api/projects/status/8ns5xutsna7cey73?svg=true)](https://ci.appveyor.com/project/felipebaltazar/podcastmanager)[![NuGet](https://img.shields.io/nuget/v/PodcastManager.svg)](https://www.nuget.org/packages/PodcastManager/)

## Current Supported Podcasts

The banks below were added in the order they are listed


| Name                                                                                                                                                                                          | Episode List | Additional Info                                                                                       | Method                               | Status |
| ---                                                                                                                                                                                           | ---     | ---                                                                                                       | ---                                  | ---    |
| [![Nerdcast](https://github.com/felipebaltazar/PodcastManager/blob/master/Logos/Nerdcast.png)](https://github.com/felipebaltazar/PodcastManager/blob/master/PodcastManager/PodcastManagers/NerdCastManager.cs)                              | Yes                 | Episode Number, Title, Published Date, Insertions         | Reversed Web API                  | OK     |
| [![NaoOuvo](https://github.com/felipebaltazar/PodcastManager/blob/master/Logos/NaoOuvo.png)](https://github.com/felipebaltazar/PodcastManager/blob/master/PodcastManager/PodcastManagers/NaoOuvoManager.cs)                              | Yes                 | Episode Number, Title, Published Date, Insertions         | Reversed Feed                  | OK     |


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
