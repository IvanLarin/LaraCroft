using LaraCroft.Entities;
using LaraCroft.ValueObjects;
using System.Text.Json;

namespace LaraCroft.Parsing;

internal class BorderJsonParser(Interval interval) : BaseParser<Border>
{
    private class Root
    {
        public Borders? Borders { get; init; }
    }

    private class Borders
    {
        public object[][]? Data { get; init; }
    }

    protected override Border DoParse(string text)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<Root>(text, options);

        var borders = root!.Borders!.Data!.Select(x => new Border
        {
            Begin = DateTime.Parse(x[0].ToString()!),
            End = DateTime.Parse(x[1].ToString()!),
            Interval = new(Int32.Parse(x[2].ToString()!))
        }).ToArray();

        var border = GetBorder(borders);

        return border;
    }

    private Border GetBorder(Border[] borders) =>
        borders.FirstOrDefault(x => x.Interval == interval) ??
        throw new Exception($"Нет границ для интервала {interval}");
}