namespace LaraCroft
{
    public class ConsoleLogger : Logger
    {
        private bool lineIsUnderUpdate;

        public void Log(string message) => WithResetUpdating(() => Console.WriteLine(message));

        public void LogSuccess(string message) => WithColor(ConsoleColor.DarkGreen, () => Log(message));

        public void LogError(string message) => WithColor(ConsoleColor.DarkRed, () => Log(message));

        public void UpdateLine(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop - (lineIsUnderUpdate ? 1 : 0));
            Console.WriteLine(message);

            lineIsUnderUpdate = true;
        }

        private void WithResetUpdating(Action whatToDo)
        {
            lineIsUnderUpdate = false;
            whatToDo();
        }

        private void WithColor(ConsoleColor color, Action whatToDo)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            whatToDo();

            Console.ForegroundColor = originalColor;
        }
    }
}
