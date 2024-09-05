namespace LaraCroft;

internal class TheInput : Input
{
    private const string FileName = "tickers.txt";

    public string[] GetTickers()
    {
        try
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            if (!File.Exists(filePath))
                CreateFile(filePath);

            string text = File.ReadAllText(filePath);

            string[] tickers = text.Split(["\r\n", "\n"], StringSplitOptions.None).Where(s => !string.IsNullOrWhiteSpace(s.Trim())).ToArray();

            if (!tickers.Any())
                throw new GoodException($"Файл с тикерами пуст. Добавьте в него тикеры через перенос строки, которые надо скачать. Файл лежит тут \"{filePath}\"");

            return tickers;
        }
        catch (Exception e)
        {
            throw new GoodException($"Ошибка при чтении файла \"{FileName}\": {e.Message}");
        }
    }

    private void CreateFile(string filePath) => File.Create(filePath).Dispose();
}