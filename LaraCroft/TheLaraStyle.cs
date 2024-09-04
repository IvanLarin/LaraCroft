namespace LaraCroft;

internal class TheLaraStyle(Lara lara, Factory factory) : Lara
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

            onSuccess?.Invoke();

            logger.LogSuccess("Миссия выполнена");
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