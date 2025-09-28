using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal interface BackDownloader : Downloader<Url, TextFromBack>;