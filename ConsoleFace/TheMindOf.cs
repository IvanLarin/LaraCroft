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

    public void BecomeCandle() => Become(new CandleMenu(this));

    public void BecomeVolume() => Become(new Volume(this));

    public void BecomeGoodbye() => Become(new Goodbye(this));

    public void BecomeSuccess() => Become(new Success(this));

    public void BecomeNirvana() => Become(new Nirvana());

    public void BecomeFail() => Become(new Fail(this));
}