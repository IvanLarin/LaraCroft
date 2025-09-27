namespace LaraCroft.Digging;

internal interface Digger<T>
{
    Task Dig(Work<T>[] works);
}