using System.Globalization;

namespace LaraCroft
{
    internal class TheExcavator(History history, PlaceToPut placeToPut, Logger logger) : Excavator
    {
        public async Task Dig(string ticker)
        {
            bool theEnd;
            int position = 0;

            do
            {
                Candle[] candles = await history.GetCandles(position);

                placeToPut.Put(candles);

                theEnd = !candles.Any();

                if (!theEnd)
                    logger.UpdateLine($"Качаю уже здесь: {candles.Last().Begin.ToString("d", new CultureInfo("ru-RU"))}");

                position += candles.Length;

            } while (!theEnd);
        }
    }
}
