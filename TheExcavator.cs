namespace LaraCroft
{
    public class TheExcavator(History history, Storage storage) : Excavator
    {
        public async Task Dig(string ticker)
        {
            bool theEnd;
            int position = 0;

            do
            {
                IList<Candle> candles = await history.GetCandlesFrom(position);

                storage.Save(candles);

                theEnd = candles.Count == 0;

            } while (!theEnd);
        }
    }
}
