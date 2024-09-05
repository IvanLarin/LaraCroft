using System.Globalization;
using System.Xml.Linq;

namespace LaraCroft;

internal class XmlCandlesParser : Parser<Candle[]>
{
    public Candle[] Parse(string text)
    {
        double ParseDouble(string str) => double.Parse(str, CultureInfo.InvariantCulture);

        try
        {
            var document = XDocument.Parse(text);

            return document.Descendants("row").Select(row => new Candle()
            {
                Begin = DateTime.Parse(row.Attribute("begin")!.Value),
                End = DateTime.Parse(row.Attribute("end")!.Value),
                Open = ParseDouble(row.Attribute("open")!.Value),
                Close = ParseDouble(row.Attribute("close")!.Value),
                High = ParseDouble(row.Attribute("high")!.Value),
                Low = ParseDouble(row.Attribute("low")!.Value),
                Volume = long.Parse(row.Attribute("volume")!.Value)
            }).ToArray();
        }
        catch (Exception e)
        {
            throw new GoodException($$"""
                                           Не удалось распарсить данные с сервера. Вот, что он вернул:
                                           {{text}}
                                           """, e);
        }
    }
}