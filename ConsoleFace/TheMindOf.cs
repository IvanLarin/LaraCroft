using LaraCroft;

namespace ConsoleFace;

internal class TheMindOf : Mind
{
    private Part part = new Nirvana();

    public TheMindOf(Lara lara)
    {
        Lara = lara;
        BecomeHello();
    }

    public Lara Lara { get; }

    public void Do() => part.Do();

    public bool Is => part.Is;

    public void BecomeHello() => Become(new Hello(this));

    private void Become(Part thePart) => part = new HandleProblemsOf(thePart, this);

    public void BecomeMainMenu() => Become(new MainMenu(this));

    public void BecomeCandleMenu() => Become(new WithAskingForTimeframe(this, timeframeInMinutes
            => Lara.DownloadCandles(timeframeInMinutes)));

    public void BecomeVolumeMenu() => Become(new WithAskingForTimeframe(this, timeframeInMinutes
            => Lara.DownloadVolumes(timeframeInMinutes)));

    public void BecomeGoodbye() => Become(new Goodbye(this));

    public void BecomeSuccess() => Become(new Success(this));

    public void BecomeNirvana() => Become(new Nirvana());
}