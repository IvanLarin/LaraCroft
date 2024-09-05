using System.Xml.Linq;

namespace LaraCroft;

internal class XmlSplitParser : SplitParser
{
    public double[] Parse(string text)
    {
        int ParseInt(string? str) => int.Parse(str ?? throw new Exception());

        try
        {
            var document = XDocument.Parse(text);

            Split[] splits = document.Descendants("row").Select(row => new Split
            {
                Before = ParseInt(row.Attribute("before")?.Value),
                After = ParseInt(row.Attribute("after")?.Value),
            }).ToArray();

            return splits.Select(split => (double)split.Before / split.After).ToArray();
        }
        catch (Exception e)
        {
            throw new GoodException($$"""
                                           Не удалось распарсить данные с сервера. Вот, что он вернул:
                                           {{text}}
                                           """, e);
        }
    }

    private record Split
    {
        public int Before { get; init; }

        public int After { get; init; }
    }
}