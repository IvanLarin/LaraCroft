namespace LaraCroft;

public class MoexHistory(string ticker, HttpClient httpClient, Parser parser) : History
{
    public async Task<Candle[]> GetCandlesFrom(int position)
    {
        string url = $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval=1&start={position}";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new TerminateException("Ошибка запроса на сервер");

        string text = await response.Content.ReadAsStringAsync();

        Candle[] candles = parser.Parse(text);

        return candles;
    }
}