namespace LaraCroft;

internal class TheSharesGetter(Downloader downloader, Parser<Share[]> parser) : SharesGetter
{
    public async Task<Share[]> GetShares() => parser.Parse(await downloader.Download("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml"));
}