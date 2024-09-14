namespace LaraCroft.ProgressTracking;

internal interface TrackerFactory<in TProgress>
{
    ProgressTracker<TProgress> MakeProgressTracker(TProgress[] initialProgress);
}