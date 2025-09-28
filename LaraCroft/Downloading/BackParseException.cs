using LaraCroft.ValueObjects;

namespace LaraCroft.Downloading;

internal class BackParseException(Url url, TextFromBack textFromBack, Exception ex)
    : Exception($"""
                 Не удалось распарсить ответ на запрос
                 {url}.

                 Бек вернул
                 {textFromBack}

                 Парсер сказал
                 {ex}
                 """);