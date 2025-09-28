using LaraCroft.ValueObjects;

namespace LaraCroft.ProgressTracking;

internal record ShareProgress
{
    public required Ticker Ticker { get; init; }

    public required DateTime Date { get; init; }

    public required bool IsCompleted { get; init; }
}