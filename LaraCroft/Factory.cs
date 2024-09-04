namespace LaraCroft;

internal interface Factory : PublicFactory
{
    Input MakeInput();

    Excavator MakeExcavator(string ticker);
}