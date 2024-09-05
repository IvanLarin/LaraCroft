namespace Common;

public static class AwesomeConsole
{
    public static void WriteLineWithColor(ConsoleColor color, object value) =>
        WithColor(color: color, doIt: () => Console.WriteLine(value: value));

    public static void WriteWithColor(ConsoleColor color, object value) =>
        WithColor(color: color, doIt: () => Console.Write(value: value));

    public static void WithColor(ConsoleColor color, Action doIt)
    {
        var originalColor = Console.ForegroundColor;

        Console.ForegroundColor = color;

        doIt();

        Console.ForegroundColor = originalColor;
    }

    public static bool Input(out string input)
    {
        input = "";

        while (true)
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                return false;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (input.Any())
                {
                    Console.WriteLine();
                    return true;
                }

                continue;
            }

            if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (input.Any())
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }

                continue;
            }

            input += keyInfo.KeyChar;
            Console.Write(value: keyInfo.KeyChar);
        }
    }
}