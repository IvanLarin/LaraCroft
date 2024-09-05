namespace LaraCroft;

public interface Lara
{
    Task DownloadCandles(int timeframeInMinutes);

    Task DownloadVolumes();
}