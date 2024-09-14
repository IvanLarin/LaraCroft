namespace ConsoleApp.Parts;

internal class Hello(Mind mind) : Part
{
    public void Do()
    {
        Console.WriteLine("""
                      Привет, я Лара Крофт - копаю историю акций с Мосбиржи.

                      Могу скачать всю доступную историю свечей в минутном таймфрейме.
                      Ещё могу скачать статистику всех акций со среднедневным объёмом в рублях.
                      """);

        mind.BecomeMainMenu();
    }
}