namespace LaraCroft.Parsing;

internal abstract class BaseParser<T> : Parser<T>
{
    public T Parse(string text)
    {
        try
        {
            return DoParse(text);
        }
        catch (Exception e)
        {
            throw new GoodException($$"""
                                      Не удалось распарсить данные с сервера. Вот, что он вернул:
                                      {{text}}
                                      """, e);
        }
    }

    protected abstract T DoParse(string text);
}