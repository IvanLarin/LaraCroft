namespace LaraCroft.ProgressTracking
{
    internal interface ProgressTracker<in TProgress> : IDisposable
    {
        void Report(TProgress shareProgress);
    }
}
