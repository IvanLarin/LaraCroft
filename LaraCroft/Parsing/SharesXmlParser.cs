using LaraCroft.Entities;
using System.Xml.Linq;

namespace LaraCroft.Parsing;

internal class SharesXmlParser : BaseParser<Share[]>
{
    protected override Share[] DoParse(string text)
    {
        var document = XDocument.Parse(text);

        return document.Descendants("data")
            .First(data => data.Attribute("id")?.Value == "securities")
            .Descendants("row").Select(row => new Share
            {
                Ticker = new(row.Attribute("SECID")!.Value),
                Name = row.Attribute("SECNAME")!.Value,
                ListingLevel = int.Parse(row.Attribute("LISTLEVEL")!.Value)
            }).ToArray();
    }
}