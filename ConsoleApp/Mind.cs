using LaraCroft;

namespace ConsoleApp;

internal interface Mind : Part
{
    Lara Lara { get; }

    void BecomeHello();

    void BecomeMainMenu();

    void BecomeCandle();

    void BecomeParameters();

    void BecomeGoodbye();

    void BecomeTotalFail();

    void BecomeSuccess();

    void BecomeNirvana();

    void BecomeFail();
}