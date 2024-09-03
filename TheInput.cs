namespace LaraCroft
{
    public class TheInput : Input
    {
        private const string FileName = "tickers.txt";

        public string[] GetTickers()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

                string text = File.ReadAllText(filePath);

                string[] tickers = text.Split(["\r\n", "\n"], StringSplitOptions.None);

                return tickers;
            }
            catch (Exception ex)
            {
                throw new TerminateException($"Ошибка при чтении файла \"{FileName}\": {ex.Message}");
            }
        }
    }
}
