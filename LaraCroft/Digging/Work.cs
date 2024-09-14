using LaraCroft.Placing;

namespace LaraCroft.Digging;

internal record Work<T>
{
    public required PlaceToPut<T> PlaceToPut { get; init; }

    public required string Ticker { get; init; }
}