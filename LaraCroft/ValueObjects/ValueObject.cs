namespace LaraCroft.ValueObjects;

public class ValueObject<T>(T value) : IEquatable<ValueObject<T>>
{
    public T Value => value;

    public override string ToString() => Value?.ToString() ?? "null";

    public override bool Equals(object? obj) => Equals(obj as ValueObject<T>);

    public bool Equals(ValueObject<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode() => Value == null ? 0 : EqualityComparer<T>.Default.GetHashCode(Value);

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) => Equals(left, right);

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) => !Equals(left, right);

    public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;
}