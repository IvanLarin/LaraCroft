namespace LaraCroft;

internal class TheStyleOf(Lara lara, Factory factory) : Lara
{
    private readonly Logger logger = factory.MakeLogger();

    public Task DownloadCandles(int timeframeInMinutes) =>
        InTheLaraStyle(() => lara.DownloadCandles(timeframeInMinutes));


    public Task DownloadVolumes(int timeframeInMinutes) =>
        InTheLaraStyle(() => lara.DownloadVolumes(timeframeInMinutes));

    private async Task InTheLaraStyle(Func<Task> doIt)
    {
        try
        {
            await doIt();
        }
        catch (GoodException e)
        {
            logger.LogError(e.Message);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError($"Необработанная ошибка: {e.Message}");
            throw;
        }
    }
}