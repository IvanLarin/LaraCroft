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
            AwesomeConsole.WriteError(exception);

            mind.BecomeMainMenu();
        }
        catch (AggregateException exception)
        {
            AwesomeConsole.WriteError(exception);

            AggregateException flat = exception.Flatten();

            var unknownExceptions = flat.InnerExceptions.Where(ex => ex is not GoodException);

            if (unknownExceptions.Any())
            {
                mind.BecomeTotalFail();
            }
            else
            {
                mind.BecomeMainMenu();
            }
        }
        catch (Exception exception)
        {
            AwesomeConsole.WriteError(exception);

            mind.BecomeTotalFail();
        }
    }

    public bool Is => part.Is;
}