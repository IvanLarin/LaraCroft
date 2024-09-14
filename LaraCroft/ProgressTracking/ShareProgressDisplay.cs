using LaraCroft.Logging;
using System.Text;

namespace LaraCroft.ProgressTracking;

internal class ShareProgressDisplay(Logger logger) : ProgressDisplay<ShareProgress>
{
    private ShareProgress[] progress = [];

    private int? remainingDaysAtStart;

    public void Display(ShareProgress[] theProgress)
    {
        progress = theProgress;
        remainingDaysAtStart ??= CalculateRemainingDays(theProgress);

        EnableTimer();
    }

    private Timer? timer;

    private void EnableTimer() => timer ??= new Timer(DoIfNotRunning(UpdateView), null, 0, 100);

    private readonly object lockObject = new();

    private TimerCallback DoIfNotRunning(Action doIt) => _ =>
    {
        if (Monitor.TryEnter(lockObject))
            try
            {
                doIt();
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
    };

    private int CalculateRemainingDays(ShareProgress[] theProgress) =>
        theProgress.Aggregate(0, func: (p, x) => p + (int)DateTime.Now.Subtract(x.Date).TotalDays);

    private void UpdateView()
    {
        CalculateVariables();
        Write();
    }

    private double percentComplete;
    private double remainingMonthsAtStart;
    private double remainingMonths;
    private int maxTickerLength;

    private void CalculateVariables()
    {
        remainingMonths = CalculateRemainingDays(progress) / DaysInMonth;
        remainingMonthsAtStart = remainingDaysAtStart.HasValue ? remainingDaysAtStart.Value / DaysInMonth : 0;
        percentComplete = (remainingMonthsAtStart - remainingMonths) / remainingMonthsAtStart * 100;
        maxTickerLength = Math.Max(maxTickerLength, progress.MaxBy(p => p.Ticker.Length)?.Ticker.Length ?? 0);
    }

    private void Write()
    {
        SetupLogger();

        Prepare();

        WriteStatistics();
        WriteLeaders();

        Flush();
    }

    private readonly StringBuilder stringBuilder = new();

    private void Prepare() => stringBuilder.Clear();

    private int maxLineWidth;

    private void Flush()
    {
        var output = PadAllStrings();

        linesHoldingLogger?.WriteLine(output);
    }

    private string PadAllStrings()
    {
        var strings = stringBuilder.ToString().Split(Environment.NewLine);
        maxLineWidth = Math.Max(maxLineWidth, strings.Max(s => s.Length));
        var outputStrings = strings.Select(s => s.PadRight(maxLineWidth));
        var output = string.Join(Environment.NewLine, outputStrings);
        return output;
    }

    private void WriteStatistics() => stringBuilder.AppendLine(
        $$"""

          Всего тикеров надо скачать: {{progress.Length}}
          Уже скачано тикеров: {{progress.Count(p => p.IsCompleted)}}
          Ещё осталось скачать тикеров: {{progress.Count(p => !p.IsCompleted)}}
          Суммарно месяцев надо скачать: {{Math.Floor(remainingMonthsAtStart)}}
          Уже скачано месяцев: {{Math.Floor(remainingMonthsAtStart - remainingMonths)}}
          Ещё осталось скачать месяцев: {{Math.Floor(remainingMonths)}}
          Завершено: {{Math.Floor(percentComplete)}}%

          """);

    private void WriteLeaders()
    {
        var topCount = Math.Min(MaxTopCount, progress.Length);

        stringBuilder.AppendLine($"Топ {topCount} лидеров закачки, которых ещё качает");

        List<ShareProgress> leaders =
            progress.Where(p => !p.IsCompleted)
                .OrderByDescending(p => p.Date)
                .Take(topCount).ToList();

        leaders.ForEach(p => stringBuilder.AppendLine(GenerateLeaderLine(p.Ticker, p.Date)));

        int lineWidth = GenerateLeaderLine(new string(' ', maxTickerLength), DateTime.Now).Length;

        Enumerable.Range(0, topCount).Take(topCount - leaders.Count)
            .Select(_ => new string(' ', lineWidth)).ToList()
            .ForEach(s => stringBuilder.AppendLine(s));
    }

    private string GenerateLeaderLine(string ticker, DateTime date) =>
        $"{ticker.PadRight(maxTickerLength)} {date.Date:dd.MM.yyyy}";

    private bool? isCursorVisible;

    private Logger? linesHoldingLogger;

    private void SetupLogger()
    {
        linesHoldingLogger ??= logger.HoldThisPosition();
        isCursorVisible ??= linesHoldingLogger?.CursorVisible;

        if (linesHoldingLogger != null)
            linesHoldingLogger.CursorVisible = false;
    }

    public void Dispose()
    {
        timer?.Dispose();

        UpdateView();

        if (isCursorVisible.HasValue && linesHoldingLogger != null)
            linesHoldingLogger.CursorVisible = isCursorVisible.Value;
    }

    private const int MaxTopCount = 10;

    private const double DaysInMonth = 30.44;
}