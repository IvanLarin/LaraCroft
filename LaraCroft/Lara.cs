namespace LaraCroft;

public interface Lara
{
    Task DownloadCandles(int interval);

    Task ShowShareParameters();
}