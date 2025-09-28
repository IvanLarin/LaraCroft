using LaraCroft.Placing;
using LaraCroft.ValueObjects;

namespace LaraCroft.Digging;

internal record Work<T>
{
    public required PlaceToPut<T> PlaceToPut { get; init; }

    public required Ticker Ticker { get; init; }
}