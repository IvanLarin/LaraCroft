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

            var excavator = factory.MakeExcavator(ticker, timeframeInMinutes,
                factory.MakeFilePlaceToPut(ticker, timeframeInMinutes));

            await excavator.Dig(ticker);
        }
    }

    public Task DownloadVolumes(int timeframeInMinutes) =>
        throw new NotImplementedException();
}