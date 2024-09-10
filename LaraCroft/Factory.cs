namespace LaraCroft;

internal interface Factory
{
    Logger MakeLogger();

    Input MakeInput();

    Excavator MakeExcavator(string ticker, int timeframeInMinutes, PlaceToPut placeToPut,
        CancellationToken token = default);

    PlaceToPut MakeFile(string ticker, int timeframeInMinutes);

    SharesGetter MakeSharesGetter(CancellationToken token = default);

    CandleBuffer MakeCandleBuffer();

    ShareStatistics MakeShareStatistics();

    Output MakeOutput();
}