using System.Text;

namespace LaraCroft;

internal class TheShareStatistics : ShareStatistics
{
    private readonly List<(Share share, Candle[] candles)> list = [];

    public void Add(Share share, Candle[] candles) => list.Add((share, candles));

    public void WriteTo(Output output)
    {
        VolumeStatistics[] statistics = CalculateStatistics();

        WriteStatistics(statistics, toOutput: output);
    }


    private VolumeStatistics[] CalculateStatistics()
    {
        var statistics = list.Select(x => new VolumeStatistics
        {
            Share = x.share,
            MiddleVolume = CalculateMiddleVolume(x.candles)
        }).ToArray();

        return statistics;
    }

    private int CalculateMiddleVolume(Candle[] candles)
    {
        if (!candles.Any()) return 0;

        Queue<double> dayVolumes = [];

        var queue = new Queue<Candle>(candles);

        DateTime fromDate = queue.Peek().Begin;

        double dayVolume = 0;
        while (queue.Any())
        {
            Candle candle = queue.Peek();

            if (fromDate.Date != candle.Begin.Date)
            {
                dayVolumes.Enqueue(dayVolume);
                dayVolume = 0;
                fromDate = candle.Begin;
                continue;
            }

            var middlePrice = (candle.High - candle.Low) / 2;
            var rubbles = candle.Volume * middlePrice;

            dayVolume += rubbles;

            queue.Dequeue();
        }

        var totalRubbles = dayVolumes.Aggregate(0.0, (sum, volume) => sum + volume);

        return (int)(totalRubbles / dayVolumes.Count);
    }

    private void WriteStatistics(VolumeStatistics[] statistics, Output toOutput)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Тикер;Название;Средний объём рублей в день");

        foreach (var item in statistics)
        {
            var row = string.Join(";", [item.Share.Ticker, item.Share.Name, item.MiddleVolume]);
            sb.AppendLine(row);
        }

        toOutput.Write(sb.ToString());
    }

    private record VolumeStatistics
    {
        public required Share Share { get; init; }

        public int MiddleVolume { get; init; }
    }

}