﻿using System.Globalization;

namespace LaraCroft;

internal class TxtFile(string ticker, int timeframeInMinutes, Config config) : PlaceToPut
{
    private bool alreadySaved;

    public void Put(Candle[] candles)
    {
        var fileName = $"{ticker}.txt";
        var filePath = Path.Combine(config.OutputDirectory, fileName);

        if (!Directory.Exists(config.OutputDirectory))
            Directory.CreateDirectory(config.OutputDirectory);

        using var writer = new StreamWriter(filePath, alreadySaved);

        if (!alreadySaved)
            writer.WriteLine("<TICKER>;<PER>;<DATE>;<TIME>;<OPEN>;<HIGH>;<LOW>;<CLOSE>;<VOL>");

        foreach (var candle in candles)
        {
            var str = string.Join(';', [
                ticker,
                timeframeInMinutes,
                candle.Begin.ToString("yyMMdd"),
                candle.Begin.ToString("HHmmss"),
                candle.Open.ToString(CultureInfo.InvariantCulture),
                candle.High.ToString(CultureInfo.InvariantCulture),
                candle.Low.ToString(CultureInfo.InvariantCulture),
                candle.Close.ToString(CultureInfo.InvariantCulture),
                candle.Volume.ToString(CultureInfo.InvariantCulture)
            ]);
            writer.WriteLine(str);
        }

        alreadySaved = true;
    }
}