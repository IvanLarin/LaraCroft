namespace LaraCroft.Entities;

internal record Share
{
    public required string Ticker { get; init; }

    public required string Name { get; init; }

    public int ListingLevel { get; init; }
}