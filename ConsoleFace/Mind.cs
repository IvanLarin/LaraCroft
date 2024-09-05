using LaraCroft;

namespace ConsoleFace;

internal interface Mind : Part
{
    Lara Lara { get; }

    void BecomeHello();

    void BecomeMainMenu();

    void BecomeCandleMenu();

    void BecomeVolumeMenu();

    void BecomeGoodbye();

    void BecomeSuccess();

    void BecomeNirvana();
}