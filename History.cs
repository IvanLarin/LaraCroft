namespace LaraCroft;

public interface History
{
    Task<Candle[]> GetCandlesFrom(int position);
}