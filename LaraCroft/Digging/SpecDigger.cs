using Common;
using LaraCroft.Downloading;
using LaraCroft.Entities;
using LaraCroft.ValueObjects;

namespace LaraCroft.Digging;

internal class SpecDigger(SpecDownloader specDownloader, BorderDownloader borderDownloader) : Digger<(Spec, Border)>
{
    public async Task Dig(Work<(Spec, Border)>[] works)
    {
        using var cts = new CancellationTokenSource();

        await works.ForEachAsync(cts.Token, body: async (work, token) =>
            work.PlaceToPut.Put(await DigSpec(work.Ticker, token))
        , onException: _ => cts.Cancel());
    }

    private async Task<(Spec, Border)> DigSpec(Ticker ticker, CancellationToken token)
    {
        var spec = await specDownloader.Download(ticker, token);


        var border = await borderDownloader.Download(ticker, token);

        return (spec, border);
    }
}