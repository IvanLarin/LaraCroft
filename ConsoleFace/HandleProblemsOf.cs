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
        catch (GoodException)
        {
        }
        catch (AggregateException e)
        {
            if (e.InnerExceptions.Any(ex => ex is not GoodException))
                mind.BecomeGoodbye();
        }
        catch (Exception)
        {
            mind.BecomeGoodbye();
        }
    }

    public bool Is => part.Is;
}