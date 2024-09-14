namespace LaraCroft.Entities;

internal record Split
{
    public DateTime Date { get; set; }

    public int Before { get; init; }

    public int After { get; init; }
}