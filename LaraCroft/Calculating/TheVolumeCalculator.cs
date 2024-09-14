using LaraCroft.Entities;
using System.Globalization;

namespace LaraCroft.Calculating;

internal class TheVolumeCalculator : VolumeCalculator
{
    public int CalculateMiddleVolume(Candle[] candles)
    {
        if (candles.Length == 0) return 0;

        var totalVolumeInRubles = candles.Aggregate(0d, func: (volumeInRubles, candle) =>
            volumeInRubles + candle.Volume * (candle.High - candle.Low) / 2);

        var begin = candles.Min(x => x.Begin).Date;

        var now = DateTime.Now;

        var weekdayCount = Enumerable.Range(0, (int)(now - begin).TotalDays)
            .Select(daysSinceBegin => IsWeekday(begin.AddDays(daysSinceBegin))).Count(isWeekday => isWeekday);

        var middleVolume = (int)(totalVolumeInRubles / weekdayCount);

        return middleVolume;
    }

    private bool IsWeekday(DateTime date) => !IsWeekend(date) && !Holidays.Contains((date.Day, date.Month));

    private bool IsWeekend(DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    private static readonly IReadOnlyList<(int Day, int Month)> Holidays = new[]
        {
            "01.01.2000", // Новый год
            "02.01.2000", // Новый год
            "07.01.2000", // Рождество
            "23.02.2000", // День защитника Отечества
            "08.03.2000", // Международный женский день
            "01.05.2000", // Праздник весны и труда
            "09.05.2000", // День Победы
            "12.06.2000", // День России
            "04.11.2000" // День народного единства
        }.Select(h => DateTime.ParseExact(h, "dd.MM.yyyy", CultureInfo.InvariantCulture))
        .Select(d => (d.Day, d.Month)).ToList();
}