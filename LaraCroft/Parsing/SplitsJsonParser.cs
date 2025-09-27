using LaraCroft.Entities;
using System.Text.Json;

namespace LaraCroft.Parsing;

internal class SplitsJsonParser : BaseParser<Split[]>
{
    private class Root
    {
        public Splits? Splits { get; init; }
    }

    private class Splits
    {
        public object[][]? Data { get; init; }
    }

    protected override Split[] DoParse(string text)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<Root>(text, options);

        var result = root!.Splits!.Data!.Select(x => new Split
        {
            Date = DateTime.Parse(x[0].ToString()!),
            Before = Int32.Parse(x[2].ToString()!),
            After = Int32.Parse(x[3].ToString()!),
        }).ToArray();

        return result;
    }
}