namespace LaraCroft;

internal class TheLara(Factory factory) : Lara
{
    private const int OneHour = 60;
    private readonly Input input = factory.MakeInput();

    private readonly Logger logger = factory.MakeLogger();

    public async Task DownloadCandles(int timeframeInMinutes)
    {
        var tickers = input.GetTickers();

        using var cts = new CancellationTokenSource();

        await Parallel.ForEachAsync(tickers, cts.Token, body: async (ticker, token) =>
        {
            try
            {
                logger.Log($"Качаю {ticker}");

                var excavator = factory.MakeExcavator(ticker, timeframeInMinutes,
                    factory.MakeFile(ticker, timeframeInMinutes), token);

                await excavator.Dig();
            }
            catch
            {
                await cts.CancelAsync();
                throw;
            }
        });
    }

    public async Task DownloadVolumes()
    {
        logger.Log("Узнаю какие вообще есть акции...");
        Share[] shares = await factory.MakeSharesGetter().GetShares();

        logger.Log($$"""
                     Их {{shares.Length}} шт. Вот какие:
                     {{string.Join("    ", shares.Select(share => share.Ticker))}}
                     """);

        var statistics = factory.MakeShareStatistics();

        foreach (var share in shares)
        {
            logger.Log($"Качаю статистику по {share.Ticker}");
            var buffer = factory.MakeCandleBuffer();
            var excavator = factory.MakeExcavator(share.Ticker, OneHour, buffer);

            await excavator.Dig();

            statistics.Add(share, buffer.Candles);
        }

        logger.Log("Вот результаты:" + Environment.NewLine);

        var output = factory.MakeOutput();
        statistics.WriteTo(output);
    }
}