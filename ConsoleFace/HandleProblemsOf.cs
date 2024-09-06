using Common;
using LaraCroft;

namespace ConsoleFace;

internal class HandleProblemsOf(Part part, Mind mind) : Part
{
    public void Do()
    {
        try
        {
            part.Do();
        }
        catch (GoodException e)
        {
            Console.WriteLine();
            AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, e.Message);
        }
        catch (AggregateException e)
        {
            var goodExceptions = e.InnerExceptions.Where(ex => ex is GoodException).ToArray();
            var unknownExceptions = e.InnerExceptions.Where(ex => ex is not GoodException).ToArray();

            foreach (var exception in goodExceptions)
            {
                Console.WriteLine();
                AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, exception.Message);
                mind.BecomeFail();
            }


            if (unknownExceptions.Any())
            {
                Console.WriteLine();
                AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, "Баги:");

                foreach (var exception in unknownExceptions)
                {
                    Console.WriteLine();
                    AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, exception.Message);
                }

                mind.BecomeGoodbye();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine();
            AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, "Баг:");
            AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, e.Message);

            mind.BecomeGoodbye();
        }
    }

    public bool Is => part.Is;
}