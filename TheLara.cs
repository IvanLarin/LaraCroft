namespace LaraCroft
{
    public class TheLara(Factory factory) : Lara
    {
        private readonly Input input = factory.MakeInput();

        public void DownloadHistory()
        {
            IList<string> tickers = input.GetTickers();

            foreach (string ticker in tickers)
            {
                Excavator excavator = factory.MakeExcavator(ticker);

                excavator.Dig(ticker);
            }
        }
    }
}
