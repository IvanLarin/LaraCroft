namespace LaraCroft;

internal interface Parser<out T>
{
    T Parse(string text);
}