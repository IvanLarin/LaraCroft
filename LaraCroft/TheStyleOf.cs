namespace LaraCroft;

internal class TheStyleOf(Lara lara, Factory factory) : Lara
{
    private readonly Logger logger = factory.MakeLogger();

    public Task DownloadCandles(Action? onSuccess = null) =>
        InTheLaraStyle(() => lara.DownloadCandles(), onSuccess);

    public Task DownloadVolumes(Action? onSuccess = null) =>
        InTheLaraStyle(() => lara.DownloadVolumes(), onSuccess);

    private async Task InTheLaraStyle(Func<Task> doIt, Action? onSuccess = null)
    {
        try
        {
            await doIt();

            logger.LogSuccess("Миссия выполнена");

            onSuccess?.Invoke();
        }
        catch (TerminateException e)
        {
            logger.LogError(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError($"Необработанная ошибка: {e.Message}");
            throw;
        }
    }
}