namespace LaraCroft;

public class TheHistory(HttpClient httpClient, string ticker, Logger logger, Parser parser) : History
{
    public async Task<IList<Candle>> GetCandlesFrom(int position)
    {
        string url = $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval=1&start={position}";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new TerminateException("Ошибка запроса на сервер");
        }

        string text = await response.Content.ReadAsStringAsync();

        IList<Candle> candles = parser.Parse(text);

        return candles;
    }
}