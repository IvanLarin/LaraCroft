namespace LaraCroft.Placing;

internal interface PlaceToPut<in T>
{
    void Put(T[] candles);
}