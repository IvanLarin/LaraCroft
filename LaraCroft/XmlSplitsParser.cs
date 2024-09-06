using System.Xml.Linq;

namespace LaraCroft;

internal class XmlSplitsParser : Parser<Split[]>
{
    public Split[] Parse(string text)
    {
        try
        {
            var document = XDocument.Parse(text);

            Split[] splits = document.Descendants("row").Select(row => new Split
            {
                Date = DateTime.Parse(row.Attribute("tradedate")!.Value),
                Before = int.Parse(row.Attribute("before")!.Value),
                After = int.Parse(row.Attribute("after")!.Value)
            }).ToArray();

            return splits;
        }
        catch (Exception e)
        {
            throw new GoodException($$"""
                                      Не удалось распарсить данные с сервера. Вот, что он вернул:
                                      {{text}}
                                      """, innerException: e);
        }
    }
}