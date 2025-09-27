using LaraCroft.Entities;
using System.Text.Json;

namespace LaraCroft.Parsing;

internal class CandlesBordersJsonParser : BaseParser<CandlesBorder[]>
{
    private class Root
    {
        public Borders? Borders { get; init; }
    }

    private class Borders
    {
        public object[][]? Data { get; init; }
    }

    protected override CandlesBorder[] DoParse(string text)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<Root>(text, options);

        var result = root!.Borders!.Data!.Select(x => new CandlesBorder
        {
            Begin = DateTime.Parse(x[0].ToString()!),
            End = DateTime.Parse(x[1].ToString()!),
            Interval = Int32.Parse(x[2].ToString()!)
        }).ToArray();

        return result;
    }
}