namespace LaraCroft;

public interface Lara
{
    Task DownloadCandles(Action? onSuccess = null);

    Task DownloadVolumes(Action? onSuccess = null);
}