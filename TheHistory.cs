using static LaraCroft.TheHistory;

namespace LaraCroft;

public class TheHistory(TheHistoryParameters parameters) : History
{
    private readonly string ticker = parameters.Ticker;
    private readonly HttpClient httpClient = parameters.HttpClient;
    private readonly Logger logger = parameters.Logger;
    private readonly Parser parser = parameters.Parser;

    public async Task<Candle[]> GetCandlesFrom(int position)
    {
        string url = $"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{ticker}/candles.xml?interval=1&start={position}";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new TerminateException("Ошибка запроса на сервер");
        }

        string text = await response.Content.ReadAsStringAsync();

        Candle[] candles = parser.Parse(text);

        return candles;
    }

    public class TheHistoryParameters
    {
        public required string Ticker { get; init; }
        public required HttpClient HttpClient { get; init; }
        public required Logger Logger { get; init; }
        public required Parser Parser { get; init; }
    }
}