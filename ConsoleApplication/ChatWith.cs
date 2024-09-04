using LaraCroft;

namespace ConsoleApplication;

internal class ChatWith(Lara lara, Action? onSuccess = null)
{
    public async Task Start()
    {
        Console.Write("""
                          Привет, я Лара Крофт - копаю историю акций с Мосбиржи.
                          
                          Могу скачать всю доступную историю свечей в минутном таймфрейме.
                          Ещё могу скачать среднедневной объём в рублях с тикерами по всем акциям.

                          Что будем делать:
                          1. Качать свечи
                          2. Качать объёмы
                          3. Выйти

                          Ведите номер пункта и нажмите Enter: 
                          """);

        while (true)
        {
            string? input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    await lara.DownloadCandles(onSuccess);
                    Console.ReadLine();
                    return;
                case "2":
                    await lara.DownloadVolumes(onSuccess);
                    Console.ReadLine();
                    return;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine("Пока!");
                    onSuccess?.Invoke();
                    return;
                default:
                    Console.Write("Это мы не будем делать. Попробуйте ещё раз: ");
                    break;
            }
        }
    }
}