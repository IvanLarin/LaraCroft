namespace LaraCroft.ProgressTracking;

internal interface ShareProgressDisplayFactory
{
    ProgressDisplay<ShareProgress> MakeProgressDisplay();
}