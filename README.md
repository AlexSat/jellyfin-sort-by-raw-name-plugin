<h1>SortByRawName Jellyfin Plugin</h1>

## About

SortByRawName replaces Name sorting by 10.8 server version.

## Detailed

In Jellyfin 10.9, sorting with transliteration has appeared. I think the Jellyfin developers were solving a problem with the Chinese language, which has two alphabets at the time of writing this plugin. However, the decision to transliterate the names of library elements during sorting led to the fact that in all languages except English, sorting began to transliterate names into Latin characters (formally, if there is at least one non-Latin character, the name is transliterated into Latin characters). Personally, this sorting is inconvenient for me, which is why this plugin appeared. It replaces the sorter by name with exactly the same one (at the time of writing the plugin - Jellyfin 10.9.7), but without transliteration.

I plan to support it as far as my limited capacity allows, so it may lag behind Jellyfin releases.

Please [create a discussion](https://github.com/AlexSat/jellyfin-sort-by-raw-name-plugin/discussions/new/choose) if needed (errors, suggestions, etc).

## Installation

### Automatic (recommended)
1. Navigate to Settings > Admin Dashboard > Plugins > Repositories
2. Add a new repository with a `Repository URL` of `https://raw.githubusercontent.com/AlexSat/jellyfin-sort-by-raw-name-plugin/master/manifest.json`. The name can be anything you like.
3. Save, and navigate to Catalogue.
4. SortByRawName should be present. Click on it and install the latest version.

### Manual

[See the official Jellyfin documentation for install instructions](https://jellyfin.org/docs/general/server/plugins/index.html#installing).

1. Download a version from the [releases tab](https://github.com/AlexSat/jellyfin-sort-by-raw-name-plugin/releases) that matches your Jellyfin version.
2. Copy the `meta.json` and `jellyfin-sort-by-raw-name-plugin.dll` files into `plugins/SortByRawName` (see above official documentation on where to find the `plugins` folder).
3. Restart your Jellyfin instance.