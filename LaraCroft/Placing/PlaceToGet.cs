namespace LaraCroft.Placing;

internal interface PlaceToGet<out T>
{
    T[] Get();
}