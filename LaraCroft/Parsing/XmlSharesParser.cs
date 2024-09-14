using LaraCroft.Entities;
using System.Xml.Linq;

namespace LaraCroft.Parsing;

internal class XmlSharesParser : Parser<Share[]>
{
    public Share[] Parse(string text)
    {
        try
        {
            var document = XDocument.Parse(text);

            return document.Descendants("data")
                .First(data => data.Attribute("id")?.Value == "securities")
                .Descendants("row").Select(row => new Share
                {
                    Ticker = row.Attribute("SECID")!.Value,
                    Name = row.Attribute("SECNAME")!.Value,
                    ListingLevel = int.Parse(row.Attribute("LISTLEVEL")!.Value)
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