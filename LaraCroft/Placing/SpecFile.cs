using LaraCroft.Configuration;
using LaraCroft.Entities;
using System.Text.Json;

namespace LaraCroft.Placing
{
    internal class SpecFile(string ticker, Config config) : PlaceToPut<(Spec, CandlesBorder)>
    {
        public void Put((Spec, CandlesBorder) data) =>
            Save(data.Item1.Decimals, data.Item1.MinStep, data.Item1.LotSize, data.Item2);

        public void Save(int decimals, double minStep, int lotSize, CandlesBorder border)
        {
            var fileName = "spec.json";
            var directoryName = Path.Combine(config.OutputDirectory, ticker);
            var filePath = Path.Combine(directoryName, fileName);

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            using var writer = new StreamWriter(filePath, false);

            var content = MakeContent(decimals, minStep, lotSize, border);

            writer.Write(content);
        }

        private string MakeContent(int decimals, double minStep, int lotSize, CandlesBorder border)
        {
            var historyLength = (border.End - border.Begin).TotalDays / 365;
            var truncatedHistoryLength = Math.Floor(historyLength * 10) / 10;

            var jsonObject = new
            {
                Decimals = decimals,
                MinStep = minStep,
                LotSize = lotSize,
                HistoryLength = truncatedHistoryLength
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize(jsonObject, options);
        }
    }
}
