namespace LaraCroft;

internal record Candle
{
    public double Open { get; init; }

    public double Close { get; init; }

    public double High { get; init; }

    public double Low { get; init; }

    public double Volume { get; init; }

    public DateTime Begin { get; init; }
}