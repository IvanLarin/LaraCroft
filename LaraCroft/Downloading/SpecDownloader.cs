using LaraCroft.Entities;
using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal interface SpecDownloader : Downloader<Ticker, Spec>;