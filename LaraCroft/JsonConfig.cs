using System.Text.Json;

namespace LaraCroft;

internal class JsonConfig : Config
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
                throw new Exception("Неправильный формат");

            OutputDirectory = parsed.OutputDirectory;
        }
        catch (Exception e)
        {
            throw new GoodException(
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

    private class ParsedConfig
    {
        public required string OutputDirectory { get; init; }
    }
}