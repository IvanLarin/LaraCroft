namespace LaraCroft;

internal class TheLara(Factory factory) : Lara
{
    private readonly Input input = factory.MakeInput();

    private readonly Logger logger = factory.MakeLogger();

    public async Task DownloadCandles(Action? onSuccess = null)
    {
        var tickers = input.GetTickers();

        foreach (var ticker in tickers)
        {
            logger.Log($"Качаю {ticker}");

            Excavator excavator = factory.MakeExcavator(ticker);

            await excavator.Dig(ticker);
        }
    }

    public Task DownloadVolumes(Action? onSuccess = null) =>
        throw new NotImplementedException();
}