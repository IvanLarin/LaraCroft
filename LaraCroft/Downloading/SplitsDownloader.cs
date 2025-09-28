using LaraCroft.Entities;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal interface SplitsDownloader : Downloader<Ticker, Split[]>;