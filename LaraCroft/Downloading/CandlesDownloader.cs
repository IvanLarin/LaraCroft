using LaraCroft.Entities;

namespace LaraCroft.Downloading;

internal interface CandlesDownloader
{
    Task<Candle[]> Download(string ticker, int fromPosition);
}