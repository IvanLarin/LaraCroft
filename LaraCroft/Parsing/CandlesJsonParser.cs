using LaraCroft.Entities;
using System.Globalization;
using System.Text.Json;

namespace LaraCroft.Parsing;

internal class CandlesJsonParser : BaseParser<Candle[]>
{
    private class Root
    {
        public Candles? Candles { get; init; }
    }

    private class Candles
    {
        public object[][]? Data { get; init; }
    }

    protected override Candle[] DoParse(string text)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<Root>(text, options);

        var result = root!.Candles!.Data!.Select(x => new Candle
        {
            Open = Double.Parse(x[0].ToString()!, CultureInfo.InvariantCulture),
            Close = Double.Parse(x[1].ToString()!, CultureInfo.InvariantCulture),
            High = Double.Parse(x[2].ToString()!, CultureInfo.InvariantCulture),
            Low = Double.Parse(x[3].ToString()!, CultureInfo.InvariantCulture),
            Volume = (long)Double.Parse(x[5].ToString()!, CultureInfo.InvariantCulture),
            Begin = DateTime.Parse(x[6].ToString()!),
            End = DateTime.Parse(x[7].ToString()!)
        }).ToArray();

        return result;
    }
}