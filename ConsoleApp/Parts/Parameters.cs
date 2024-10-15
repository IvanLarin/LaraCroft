namespace ConsoleApp.Parts;

internal class Parameters(Mind mind) : Part
{
    public void Do()
    {
        mind.Lara.ShowShareParameters().Wait();

        mind.BecomeSuccess();
    }
}