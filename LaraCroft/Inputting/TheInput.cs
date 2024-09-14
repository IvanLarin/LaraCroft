using LaraCroft.Logging;

namespace LaraCroft.Inputting;

internal class TheInput(Logger logger) : Input
{
    private const string FileName = "tickers.txt";

    public string[] GetTickers()
    {
        try
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            CreateFileIfRequired(filePath);

            var text = File.ReadAllText(filePath);

            var tickers = text.Split(["\r\n", "\n"], StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s.Trim())).ToArray();

            if (!tickers.Any())
                throw new GoodException(
                    $"Файл с тикерами пуст. Добавьте в него тикеры через перенос строки, которые надо скачать. Файл лежит тут \"{filePath}\"");

            return tickers;
        }
        catch (Exception e)
        {
            throw new GoodException($"Ошибка при чтении файла \"{FileName}\": {e.Message}");
        }
    }

    private void CreateFileIfRequired(string filePath)
    {
        if (File.Exists(filePath)) return;

        logger.WriteErrorLine("Файл с тикерами на закачку был пуст и его пришлось создать.");

        logger.WriteLine($$"""
                           Сейчас в нём акции ВТБ, Газпрома и Сбера для примера.
                           Отредактируйте файл, чтобы там были тикеры акций, история которых вам нужна.
                           Файл лежит тут "{{filePath}}"
                           """);

        File.WriteAllText(filePath,
            """
            VTBR
            GAZP
            SBER
            """);
    }
}