using LaraCroft;

namespace ConsoleFace;

internal interface Mind : Part
{
    Lara Lara { get; }

    void BecomeHello();

    void BecomeMainMenu();

    void BecomeCandle();

    void BecomeVolume();

    void BecomeGoodbye();

    void BecomeSuccess();

    void BecomeNirvana();
}