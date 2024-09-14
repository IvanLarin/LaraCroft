using LaraCroft.Entities;

namespace LaraCroft.Downloading;

internal interface SharesDownloader
{
    Task<Share[]> Download();
}