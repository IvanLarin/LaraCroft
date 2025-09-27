using LaraCroft.Entities;
using System.Globalization;
using System.Text.Json;

namespace LaraCroft.Parsing;

internal class SpecJsonParser : BaseParser<Spec>
{
    private class Root
    {
        public Securities? Securities { get; init; }
    }

    private class Securities
    {
        public object[][]? Data { get; init; }
    }

    protected override Spec DoParse(string text)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<Root>(text, options);

        var result = root!.Securities!.Data!.Select(x => new Spec
        {
            LotSize = Int32.Parse(x[4].ToString()!),
            Decimals = Int32.Parse(x[8].ToString()!),
            MinStep = Double.Parse(x[14].ToString()!, CultureInfo.InvariantCulture),
        }).ToArray()[0];

        return result;
    }
}