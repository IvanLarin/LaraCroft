namespace ConsoleFace;

internal class Volume(Mind mind) : Part
{
    public void Do()
    {
        mind.Lara.DownloadVolumes().Wait();

        mind.BecomeSuccess();
    }
}