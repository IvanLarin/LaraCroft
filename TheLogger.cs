namespace LaraCroft
{
    public class TheLogger : Logger
    {
        public void Log(string message) => Console.WriteLine(message);

        public void LogSuccess(string message) => DoWithColor(ConsoleColor.DarkGreen, () => Console.WriteLine(message));

        public void LogError(string message) => DoWithColor(ConsoleColor.DarkRed, () => Console.WriteLine(message));

        private void DoWithColor(ConsoleColor color, Action whatToDo)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            whatToDo();

            Console.ForegroundColor = originalColor;
        }
    }
}
