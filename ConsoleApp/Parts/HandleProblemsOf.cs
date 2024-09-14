using Common;
using LaraCroft;

namespace ConsoleApp.Parts;

internal class HandleProblemsOf(Part part, Mind mind) : Part
{
    public void Do()
    {
        try
        {
            part.Do();
        }
        catch (GoodException exception)
        {
            LogException(exception);

            mind.BecomeMainMenu();
        }
        catch (AggregateException exception)
        {
            GoodException[] goodExceptions = exception.InnerExceptions.OfType<GoodException>().ToArray();
            Exception[] unknownExceptions = exception.InnerExceptions.Where(ex => ex is not GoodException).ToArray();

            Array.ForEach(goodExceptions, LogException);

            if (unknownExceptions.Any())
            {
                Array.ForEach(unknownExceptions, LogException);

                mind.BecomeTotalFail();
            }
            else
            {
                mind.BecomeMainMenu();
            }
        }
        catch (Exception exception)
        {
            LogException(exception);

            mind.BecomeTotalFail();
        }
    }

    private void LogException(Exception exception)
    {
        Console.WriteLine();

        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, "Баг:");
        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, exception.Message);

        if (exception.StackTrace != null)
            AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, exception);

        if (exception.InnerException != null)
            LogException(exception.InnerException);
    }

    private void LogException(GoodException exception)
    {
        Console.WriteLine();

        AwesomeConsole.WriteLineWithColor(ConsoleColor.DarkRed, exception.Message);
    }

    public bool Is => part.Is;
}