namespace LaraCroft.ValueObjects;

public class Interval : ValueObject<int>
{
    public Interval(int interval) : base(interval) => Check(interval);

    private static void Check(int value)
    {
        if (!AllowedValues.ContainsKey(value))
        {
            string allowedDescriptions = string.Join(", ", AllowedValues
                .Select(kvp => $"{kvp.Key} ({kvp.Value})"));

            throw new ArgumentOutOfRangeException(nameof(value),
                $"Недопустимое значение интервала: {value}. " +
                $"Разрешены только следующие значения: {allowedDescriptions}.");
        }
    }

    private static readonly Dictionary<int, string> AllowedValues = new()
    {
        { 1, "минута (60 секунд)" },
        { 10, "10 минут (600 секунд)" },
        { 60, "час (3600 секунд)" },
        { 24, "день (86400 секунд)" },
        { 7, "неделя (604800 секунд)" },
        { 31, "месяц (приблизительно, 2678400 секунд)" },
        { 4, "квартал (около трёх месяцев, 8035200 секунд)" }
    };
}