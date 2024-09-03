namespace LaraCroft;

public class FileStorage(string ticker, Config config) : Storage
{
    private bool alreadySaved;

    public void Save(Candle[] candles)
    {
        string fileName = $"{ticker}.csv";
        string filePath = Path.Combine(config.OutputDirectory, fileName);

        using var writer = new StreamWriter(filePath, append: alreadySaved);

        foreach (var candle in candles)
        {
            string str = string.Join(';', [candle.Begin, candle.End, candle.Low, candle.High, candle.Close, candle.Open, candle.Volume]);
            writer.WriteLine(str);
        }

        alreadySaved = true;
    }
}