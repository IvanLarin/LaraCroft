using LaraCroft.Entities;

namespace LaraCroft.Downloading;

internal interface CandlesDownloader : Downloader<CandlesDownloaderProps, Candle[]>;