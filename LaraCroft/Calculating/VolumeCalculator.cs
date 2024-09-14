using LaraCroft.Entities;

namespace LaraCroft.Calculating;

internal interface VolumeCalculator
{
    int CalculateMiddleVolume(Candle[] candles);
}