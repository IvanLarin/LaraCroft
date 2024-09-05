namespace LaraCroft;

internal interface Factory
{
    Logger MakeLogger();

    Input MakeInput();

    Excavator MakeExcavator(string ticker, int timeframeInMinutes, PlaceToPut placeToPut);

    PlaceToPut MakeFilePlaceToPut(string ticker, int timeframeInMinutes);
}