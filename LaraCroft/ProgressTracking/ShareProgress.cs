namespace LaraCroft.ProgressTracking;

internal record ShareProgress
{
    public required string Ticker { get; init; }

    public required DateTime Date { get; init; }

    public required bool IsCompleted { get; init; }
}