namespace LaraCroft;

internal class TheSharesGetter(HttpClient httpClient, Parser<Share[]> parser) : SharesGetter
{
    public async Task<Share[]> GetShares()
    {
        var url = "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new GoodException($"Ошибка GET запроса на сервер по такому URL: \"{url}\"");

        string text = await response.Content.ReadAsStringAsync();

        Share[] shares = parser.Parse(text);

        return shares;
    }
}