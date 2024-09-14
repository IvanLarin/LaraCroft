namespace ConsoleApp.Parts;

internal class Volume(Mind mind) : Part
{
    public void Do()
    {
        mind.Lara.ShowShares().Wait();

        mind.BecomeSuccess();
    }
}