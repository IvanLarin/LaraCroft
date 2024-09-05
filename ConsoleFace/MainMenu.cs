using Common;

namespace ConsoleFace;

internal class MainMenu(Mind mind) : Part
{
    public void Do()
    {
        Console.Write("""
                      
                      Что будем делать:
                      1. Качать свечи
                      2. Качать объёмы
                      3. Выйти (Esc)

                      Введите номер пункта и нажмите Enter: 
                      """);

        while (true)
        {
            if (!AwesomeConsole.Input(out var input))
            {
                Console.WriteLine();

                mind.BecomeGoodbye();
                return;
            }

            input = input.Trim();

            switch (input)
            {
                case "1":
                    mind.BecomeCandle();
                    return;
                case "2":
                    mind.BecomeVolume();
                    return;
                case "3":
                    mind.BecomeGoodbye();
                    return;
                default:
                    Console.Write("Это мы не будем делать. Попробуйте ещё раз: ");
                    break;
            }
        }
    }
}