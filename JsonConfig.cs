using System.Text.Json;

namespace LaraCroft;

public class JsonConfig : Config
{

    public JsonConfig(Constants constants)
    {
        try
        {
            if (!File.Exists(constants.ConfigFilePath))
                File.WriteAllText(constants.ConfigFilePath, constants.DefaultConfigContent);

            string jsonString = File.ReadAllText(constants.ConfigFilePath);
            ParsedConfig? parsed = JsonSerializer.Deserialize<ParsedConfig>(jsonString);

            if (parsed == null)
                throw new TerminateException();

            OutputDirectory = parsed.OutputDirectory;
            CandleDurationInMinutes = parsed.CandleDurationInMinutes;
        }
        catch (Exception e)
        {
            throw new TerminateException(
                $$"""
                Ошибка чтения конфига: "{{e.Message}}"
                
                Конфиг должен находиться тут: {{constants.ConfigFilePath}}
                Он должен иметь такой формат:
                {{constants.DefaultConfigContent}}
                Если его удалить, то программа при старте сделает новый правильный
                """,
            e);
        }
    }
    public string OutputDirectory { get; }
    public int CandleDurationInMinutes { get; }

    private class ParsedConfig
    {
        public required string OutputDirectory { get; init; }

        public required int CandleDurationInMinutes { get; init; }
    }
}