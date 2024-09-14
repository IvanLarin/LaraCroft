namespace LaraCroft.Parsing;

internal interface Parser<out T>
{
    T Parse(string text);
}