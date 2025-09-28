using LaraCroft.Configuration;
using LaraCroft.Entities;
using System.Globalization;

namespace LaraCroft.Placing
{
    internal class SpecFile(string ticker, Config config) : PlaceToPut<(Spec, Border)>
    {
        public void Put((Spec, Border) data) =>
            Save(data.Item1.Decimals, data.Item1.MinStep, data.Item1.LotSize, data.Item2);

        public void Save(int decimals, double minStep, int lotSize, Border border)
        {
            var fileName = "spec.spec";
            var directoryName = Path.Combine(config.OutputDirectory, ticker);
            var filePath = Path.Combine(directoryName, fileName);

            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            using var writer = new StreamWriter(filePath, false);

            var content = MakeContent(decimals, minStep, lotSize, border);

            writer.Write(content);
        }

        private string MakeContent(int decimals, double minStep, int lotSize, Border border)
        {
            var historyLength = (border.End - border.Begin).TotalDays / 365;
            var truncatedHistoryLength = Math.Floor(historyLength * 10) / 10;

            var text =
                $""" 
                Тикер: {ticker}
                Имя: Lara_{ticker}
                Количество знаков: {decimals}
                Кол-во денежных знаков: 2
                Шаг цены: {minStep.ToString(CultureInfo.InvariantCulture)}
                Размер лота: {lotSize}
                Лет истории: {truncatedHistoryLength.ToString(CultureInfo.InvariantCulture)}
                """;

            return text;
        }
    }
}
