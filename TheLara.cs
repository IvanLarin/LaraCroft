namespace LaraCroft
{
    public class TheLara(Factory factory) : Lara
    {
        private readonly Input input = factory.MakeInput();

        private readonly Logger logger = factory.MakeLogger();

        public async Task DownloadHistory(Action? onSuccess = null)
        {
            try
            {
                await Download();

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

        private async Task Download()
        {
            string[] tickers = input.GetTickers();

            foreach (string ticker in tickers)
            {
                logger.Log($"Качаю {ticker}");

                Excavator excavator = factory.MakeExcavator(ticker);

                await excavator.Dig(ticker);
            }
        }
    }
}
