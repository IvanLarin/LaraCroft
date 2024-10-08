﻿using LaraCroft.Digging;
using LaraCroft.Entities;
using LaraCroft.Inputting;
using LaraCroft.Logging;
using LaraCroft.Placing;

namespace LaraCroft;

internal class TheLara(Factory factory, Input input, Logger logger) : Lara
{
    private const int OneHour = 60;

    public async Task DownloadCandles(int timeframeInMinutes)
    {
        var tickers = input.GetTickers();

        Work<Candle>[] works = tickers.Select(ticker => new Work<Candle>
        {
            PlaceToPut = factory.MakeFile(ticker, timeframeInMinutes),
            Ticker = ticker
        }).ToArray();

        await factory.MakeDigger().Dig(works, timeframeInMinutes);
    }

    public async Task ShowShares()
    {
        logger.WriteLine("Узнаю какие акции есть вообще...");

        Share[] shares = await GetShares();

        (Place<Candle> Place, Share Share)[] places =
            shares.Select(share => (Place: factory.MakeCandlePlace(), Share: share)).ToArray();

        Work<Candle>[] works = places.Select(work => new Work<Candle>
        {
            PlaceToPut = work.Place,
            Ticker = work.Share.Ticker
        }).ToArray();

        await factory.MakeDigger().Dig(works, OneHour);

        var calculator = factory.MakeVolumeCalculator();

        (Share Share, int Volume, DateTime? Begin)[] statistics = places.Select(work =>
            (work.Share,
                Volume: calculator.CalculateAverageVolume(work.Place.Get()),
                Begin: GetBegin(work.Place.Get()))).ToArray();

        WriteStatistics(statistics);
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
            return await factory.MakeSharesDownloader(cts.Token).Download();
        }
        catch
        {
            await cts.CancelAsync();
            throw;
        }
    }

    private void WriteStatistics((Share Share, int Volume, DateTime? Begin)[] statistics)
    {
        logger.WriteLine("Вот результаты. Это CSV:");
        logger.WriteLine();

        logger.WriteLine("Тикер;Название;Средний объём рублей в день;Уровень листинга;Дата начала истории");

        Array.ForEach(statistics, action: s => logger.WriteLine(
            string.Join(";", [s.Share.Ticker, s.Share.Name, s.Volume, s.Share.ListingLevel, $"{s.Begin:dd.MM.yyyy}"])));
    }
}