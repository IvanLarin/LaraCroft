namespace LaraCroft;

public interface Storage
{
    void Save(IList<Candle> candles);
}