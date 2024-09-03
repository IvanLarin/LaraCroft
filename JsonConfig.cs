using System.Text.Json;

namespace LaraCroft;

public class JsonConfig : Config
{

    public JsonConfig(Constants constants)
    {
        try
        {
            string jsonString = File.ReadAllText(constants.ConfigFilePath);
            ParsedConfig? parsed = JsonSerializer.Deserialize<ParsedConfig>(jsonString);

            if (parsed == null)
                throw new TerminateException();

            OutputDirectory = parsed.OutputDirectory;
        }
        catch (Exception e)
        {
            throw new TerminateException(
                $$"""
                Ошибка чтения конфига: "{{e.Message}}"
                
                Конфиг должен находиться тут: {{constants.ConfigFilePath}}
                Он должен иметь такой формат:
                {
                  "OutputDirectory": "C:\Data"
                }
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