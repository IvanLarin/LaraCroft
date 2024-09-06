namespace Common;

public static class Fp
{
    public static TRight Pipe<TLeft, TRight>(this TLeft left, Func<TLeft, TRight> right) => right(left);

    public static void Pipe<TLeft>(this TLeft left, Action<TLeft> right) => right(left);

    public static async Task<TRight> Pipe<TLeft, TRight>(this Task<TLeft> left, Func<TLeft, Task<TRight>> right) =>
        await right(await left);

    public static async Task<TRight> Pipe<TLeft, TRight>(this Task<TLeft> left, Func<TLeft, TRight> right) =>
        right(await left);

    public static async Task Pipe<TLeft>(this Task<TLeft> left, Func<TLeft, Task> right) => await right(await left);

    public static async void Pipe<TLeft>(this Task<TLeft> left, Action<TLeft> right) => right(await left);

    public static Task<TRight> Pipe<TLeft, TRight>(this Func<Task<TLeft>> left,
        Func<Func<Task<TLeft>>, Task<TRight>> right) => right(left);
}