namespace Common;

public static class ParallelExtensions
{
    public static async Task ForEachAsync<TSource>(this IEnumerable<TSource> source,
        CancellationToken cancellationToken,
        Func<TSource, CancellationToken, ValueTask> body, Action<Exception>? onException = null)
    {
        Stack<Exception> exceptions = new();

        try
        {
            await Parallel.ForEachAsync(source, cancellationToken, body: async (sourceItem, token) =>
            {
                try
                {
                    await body(sourceItem, token);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    exceptions.Push(exception);
                    onException?.Invoke(exception);
                }
            });
        }
        catch
        {
            if (exceptions.Any())
                throw new AggregateException(exceptions);

            throw;
        }
    }
}