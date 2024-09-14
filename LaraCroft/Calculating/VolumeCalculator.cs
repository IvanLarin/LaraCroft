using LaraCroft.Entities;

namespace LaraCroft.Calculating;

internal interface VolumeCalculator
{
    int CalculateAverageVolume(Candle[] candles);
}