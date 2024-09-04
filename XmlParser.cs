using System.Globalization;
using System.Xml.Linq;

namespace LaraCroft;

public class XmlParser : Parser
{
    public Candle[] Parse(string text)
    {
        DateTime ParseDateTime(string? str) => DateTime.Parse(str ?? throw new Exception());

        double ParseDouble(string? str) => double.Parse(str ?? throw new Exception(), CultureInfo.InvariantCulture);

        try
        {
            var document = XDocument.Parse(text);

            return document.Descendants("row").Select(row => new Candle()
            {
                Begin = ParseDateTime(row.Attribute("begin")?.Value),
                Open = ParseDouble(row.Attribute("open")?.Value),
                Close = ParseDouble(row.Attribute("close")?.Value),
                High = ParseDouble(row.Attribute("high")?.Value),
                Low = ParseDouble(row.Attribute("low")?.Value),
                Volume = ParseDouble(row.Attribute("volume")?.Value)
            }).ToArray();
        }
        catch (Exception e)
        {
            throw new TerminateException($$"""
                                           Не удалось распарсить данные с сервера. Вот, что он вернул:
                                           {{text}}
                                           """, e);
        }
    }
}