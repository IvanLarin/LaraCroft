using System.Text.Json;

namespace LaraCroft;

public class JsonInput() : Input
{
    private const string FileName = "config.json";

    public IList<string> GetTickers()
    {
        try
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);

            string jsonString = File.ReadAllText(filePath);

            var config = JsonSerializer.Deserialize<Config>(jsonString, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (config == null)
                throw new TerminateException($"У {FileName} неправильное содержимое");

            return config.Tickers;
        }
        catch (Exception ex)
        {
            throw new TerminateException($"Ошибка при чтении файла конфига: {ex.Message}");
        }
    }

    private class Config
    {
        public required IList<string> Tickers { get; init; }
    }
}