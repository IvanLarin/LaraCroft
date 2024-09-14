namespace LaraCroft.Logging;

internal class HoldPositionLogger(Logger logger) : Logger
{
    public void WriteLine(string value = "") => DoFromPosition(logger.WriteLine)(value);

    public void WriteErrorLine(string value = "") => DoFromPosition(logger.WriteErrorLine)(value);

    public Logger HoldThisPosition() => this;

    public (int Left, int Top) GetCursorPosition() => occupiedPosition ?? logger.GetCursorPosition();

    public bool CursorVisible
    {
        get => logger.CursorVisible;
        set => logger.CursorVisible = value;
    }

    public void SetCursorPosition(int left, int top)
    {
        if (occupiedPosition == null)
            logger.SetCursorPosition(left, top);
    }

    private Action<string> DoFromPosition(Action<string> doIt) => value =>
    {
        var currentPosition = logger.GetCursorPosition();

        if (currentPosition != lastEnd)
        {
            CensorPreviousOutputIfRequired();
            HoldThisPosition(currentPosition);
        }
        else
        {
            GoToPosition(occupiedPosition);
        }

        doIt(value);

        lastEnd = logger.GetCursorPosition();
        lastOutput = value;
    };

    private (int Left, int Top)? occupiedPosition;

    private (int Left, int Top)? lastEnd;

    private string? lastOutput;

    private void HoldThisPosition((int Left, int Top) position) => occupiedPosition = position;

    private void GoToPosition((int Left, int Top)? position)
    {
        if (position.HasValue)
            logger.SetCursorPosition(position.Value.Left, position.Value.Top);
    }

    private void CensorPreviousOutputIfRequired()
    {
        var currentPosition = logger.GetCursorPosition();

        if (occupiedPosition.HasValue && lastEnd.HasValue &&
            lastEnd.Value.Top <= currentPosition.Top && lastOutput != null)
        {
            GoToPosition(occupiedPosition);

            var trimmedStrings = lastOutput.Split(Environment.NewLine).Select(s => s.TrimEnd());
            string censor = string.Join(Environment.NewLine, trimmedStrings.Select(s => new string('\u25a0', s.Length)));

            logger.WriteLine(censor);

            GoToPosition(currentPosition);
        }
    }
}