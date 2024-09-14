namespace LaraCroft.Entities;

internal record Candle
{
    public double Open { get; init; }

    public double Close { get; init; }

    public double High { get; init; }

    public double Low { get; init; }

    public long Volume { get; init; }

    public DateTime Begin { get; init; }

    public DateTime End { get; init; }
}