using LaraCroft.ValueObjects;

namespace LaraCroft;

public interface Lara
{
    Task DownloadCandles(Interval interval);

    Task ShowShareParameters();
}