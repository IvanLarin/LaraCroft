using Common;

namespace ConsoleFace;

internal class WithAskingForTimeframe(Mind mind, Func<int, Task> doIt) : Part
{
    public void Do()
    {
        Console.WriteLine();
        Console.Write("Введите длину свечи в минутах и нажите Enter: ");

        while (true)
        {
            if (!AwesomeConsole.Input(out var input))
            {
                Console.WriteLine();

                mind.BecomeMainMenu();
                return;
            }

            input = input.Trim();

            if (int.TryParse(input, out var timeframeInMinutes) && timeframeInMinutes >= 1)
            {
                doIt(timeframeInMinutes).Wait();

                mind.BecomeSuccess();

                return;
            }

            Console.Write("Не пойдёт. Нужно целое число больше нуля. Попробуйте ещё раз: ");
        }
    }
}