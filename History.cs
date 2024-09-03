namespace LaraCroft;

public interface History
{
    Task<IList<Candle>> GetCandlesFrom(int position);
}