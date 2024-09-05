namespace LaraCroft;

internal interface SharesGetter
{
    Task<Share[]> GetShares();
}