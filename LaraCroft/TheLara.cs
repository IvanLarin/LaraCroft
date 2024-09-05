namespace LaraCroft;

internal class TheLara(Factory factory) : Lara
{
    private readonly Input input = factory.MakeInput();

    private readonly Logger logger = factory.MakeLogger();

    public async Task DownloadCandles(int timeframeInMinutes)
    {
        var tickers = input.GetTickers();

        foreach (var ticker in tickers)
        {
            logger.Log($"Качаю {ticker}");

            Excavator excavator = factory.MakeExcavator(ticker, timeframeInMinutes,
                factory.MakeFilePlaceToPut(ticker, timeframeInMinutes));

            await excavator.Dig();
        }
    }

    public async Task DownloadVolumes()
    {
        logger.Log("Узнаю какие вообще есть акции...");
        Share[] shares = await factory.MakeSharesGetter().GetShares();

        logger.Log($$"""
                   Вот какие:
                   {{string.Join(Environment.NewLine, shares.Select(share => share.Ticker))}}
                   """);

        ShareStatistics statistics = factory.MakeShareStatistics();

        foreach (var share in shares)
        {
            logger.Log($"Качаю статистику по {share.Ticker}");
            CandleBuffer buffer = factory.MakeCandleBuffer();
            Excavator excavator = factory.MakeExcavator(share.Ticker, OneHour, placeToPut: buffer);

            await excavator.Dig();

            statistics.Add(share, buffer.Candles);
        }

        logger.Log("Вот результаты:" + Environment.NewLine);

        Output output = factory.MakeOutput();
        statistics.WriteTo(output);
    }

    private const int OneHour = 60;
}