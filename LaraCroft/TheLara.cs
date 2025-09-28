using LaraCroft.Digging;
using LaraCroft.Entities;
using LaraCroft.Inputting;
using LaraCroft.Logging;
using LaraCroft.Placing;
using LaraCroft.ValueObjects;

namespace LaraCroft;

internal class TheLara(Factory factory, Input input, Logger logger) : Lara
{
    private Interval OneHour { get; } = new Interval(60);

    public async Task DownloadCandles(Interval interval)
    {
        var tickers = input.GetTickers();

        logger.WriteLine("Начинаю. Сейчас пойдёт инфа...");

        await DownloadSpecs(tickers, interval);

        await DigCandles(tickers, interval);
    }

    private async Task DigCandles(Ticker[] tickers, Interval interval)
    {
        Work<Candle[]>[] works = tickers.Select(ticker => new Work<Candle[]>
        {
            PlaceToPut = factory.MakeCandlePlace(ticker, interval),
            Ticker = ticker
        }).ToArray();

        await factory.MakeCandleDigger(interval).Dig(works);
    }

    private async Task DownloadSpecs(Ticker[] tickers, Interval interval)
    {
        Work<(Spec, Border)>[] works = tickers.Select(ticker => new Work<(Spec, Border)>
        {
            PlaceToPut = factory.MakeSpecPlace(ticker),
            Ticker = ticker
        }).ToArray();

        await factory.MakeSpecDigger(interval).Dig(works);
    }

    public async Task ShowShareParameters()
    {
        logger.WriteLine("Узнаю какие акции есть вообще...");

        Share[] shares = await GetShares();

        (Place<Candle[]> Place, Share Share)[] places =
            shares.Select(share => (Place: factory.MakeInMemoryCandlePlace(), Share: share)).ToArray();

        Work<Candle[]>[] works = places.Select(work => new Work<Candle[]>
        {
            PlaceToPut = work.Place,
            Ticker = work.Share.Ticker
        }).ToArray();

        await factory.MakeCandleDigger(OneHour).Dig(works);

        var calculator = factory.MakeVolumeCalculator();

        (Share Share, int Volume, DateTime? Begin)[] parameters = places.Select(work =>
            (work.Share,
                Volume: calculator.CalculateAverageVolume(work.Place.Get()),
                Begin: GetBegin(work.Place.Get()))).ToArray();

        WriteParameters(parameters);
    }

    private DateTime? GetBegin(Candle[] candles)
    {
        if (candles.Any())
            return candles.Min(candle => candle.Begin);
        return null;
    }

    private async Task<Share[]> GetShares()
    {
        using var cts = new CancellationTokenSource();

        try
        {
            return await factory.MakeSharesDownloader().Download(new(), cts.Token);
        }
        catch
        {
            await cts.CancelAsync();
            throw;
        }
    }

    private void WriteParameters((Share Share, int Volume, DateTime? Begin)[] parameters)
    {
        logger.WriteLine("Вот результаты. Это CSV:");
        logger.WriteLine();

        logger.WriteLine("Тикер;Название;Средний объём рублей в день;Уровень листинга;Дата начала истории");

        Array.ForEach(parameters, action: s => logger.WriteLine(
            string.Join(";", [s.Share.Ticker, s.Share.Name, s.Volume, s.Share.ListingLevel, $"{s.Begin:dd.MM.yyyy}"])));
    }
}