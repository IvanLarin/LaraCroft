namespace LaraCroft.ProgressTracking;

internal interface ProgressDisplay<in T> : IDisposable
{
    void Display(T[] progress);
}