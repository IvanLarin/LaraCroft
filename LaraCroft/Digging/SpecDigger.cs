using Common;
using LaraCroft.Entities;
using LaraCroft.Parsing;

namespace LaraCroft.Digging;

internal class SpecDigger(int interval, Factory factory, Parser<Spec> specParser, Parser<CandlesBorder[]> candlesBordersParser) : Digger<(Spec, CandlesBorder)>
{
    public async Task Dig(Work<(Spec, CandlesBorder)>[] works)
    {
        using var cts = new CancellationTokenSource();

        await works.ForEachAsync(cts.Token, body: async (work, token) =>
            work.PlaceToPut.Put(await DigSpec(work.Ticker, token))
        , onException: _ => cts.Cancel());
    }

    private async Task<(Spec, CandlesBorder)> DigSpec(string ticker, CancellationToken token)
    {
        var downloader = factory.MakeDownloader(token);

        string specJson =
            await downloader.Download(
                $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}.json");

        Spec spec = specParser.Parse(specJson);

        string bordersJson = await downloader.Download(
            $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candleborders.json");

        CandlesBorder[] borders = candlesBordersParser.Parse(bordersJson);

        var border = GetBorder(borders);

        return (spec, border);
    }


    private CandlesBorder GetBorder(CandlesBorder[] borders) =>
        borders.FirstOrDefault(x => x.Interval == interval) ??
        throw new Exception($"Нет границ для интервала {interval}");
}