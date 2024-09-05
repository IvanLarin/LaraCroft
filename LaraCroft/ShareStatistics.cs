namespace LaraCroft;

internal interface ShareStatistics
{
    void Add(Share share, Candle[] candles);

    void WriteTo(Output output);
}