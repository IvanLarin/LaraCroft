using System.Text.Json;

namespace LaraCroft;

internal class JsonConfig : Config
{
    public JsonConfig()
    {
        try
        {
            if (!File.Exists(ConfigFilePath))
                File.WriteAllText(ConfigFilePath, DefaultConfigContent);

            var jsonString = File.ReadAllText(ConfigFilePath);
            var parsed = JsonSerializer.Deserialize<Config>(jsonString);

            if (parsed == null)
                throw new Exception("Неправильный формат");

            OutputDirectory = parsed.OutputDirectory;
            TryCountToDownload = parsed.TryCountToDownload;
            DelayBetweenTriesInMilliseconds = parsed.DelayBetweenTriesInMilliseconds;
        }
        catch (Exception e)
        {
            throw new GoodException(
                $$"""
                  Ошибка чтения конфига: "{{e.Message}}"

                  Конфиг должен находиться тут: {{ConfigFilePath}}
                  
                  Он должен иметь такой формат:
                  {{DefaultConfigContent}}
                  
                  Если его удалить, то программа при старте сделает новый правильный
                  
                  """,
                e);
        }
    }

    private static string ConfigFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

    private static readonly string DefaultConfigContent = JsonSerializer.Serialize(
        new Config
        {
            OutputDirectory = ".\\History",
            TryCountToDownload = 40,
            DelayBetweenTriesInMilliseconds = 5
        },
        new JsonSerializerOptions
        {
            WriteIndented = true
        });

    public string OutputDirectory { get; }

    public int TryCountToDownload { get; }

    public int DelayBetweenTriesInMilliseconds { get; }

    private class Config
    {
        public required string OutputDirectory { get; init; }

        public required int TryCountToDownload { get; init; }

        public required int DelayBetweenTriesInMilliseconds { get; init; }
    }
}