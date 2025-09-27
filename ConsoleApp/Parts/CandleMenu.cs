using Common;

namespace ConsoleApp.Parts;

internal class CandleMenu(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine();
        Console.Write("Введите интервал и нажите Enter: ");

        while (true)
        {
            if (!AwesomeConsole.Input(out var input))
            {
                Console.WriteLine();

                mind.BecomeMainMenu();
                return;
            }

            input = input.Trim();

            if (int.TryParse(input, out var interval) && interval >= 1)
            {
                mind.Lara.DownloadCandles(interval).Wait();

                mind.BecomeSuccess();

                return;
            }

            Console.Write("Не пойдёт. Нужно целое число больше нуля. Попробуйте ещё раз: ");
        }
    }
}