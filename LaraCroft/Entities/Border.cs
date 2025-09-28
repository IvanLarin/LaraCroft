using LaraCroft.ValueObjects;

namespace LaraCroft.Entities;

internal class Border
{
    public required DateTime Begin { get; init; }

    public required DateTime End { get; init; }

    public required Interval Interval { get; init; }
}