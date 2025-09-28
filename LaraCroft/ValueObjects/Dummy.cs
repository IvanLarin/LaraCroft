namespace LaraCroft.ValueObjects;

public class Dummy() : ValueObject<object>(DummyObject)
{
    private static readonly object DummyObject = new object();
}