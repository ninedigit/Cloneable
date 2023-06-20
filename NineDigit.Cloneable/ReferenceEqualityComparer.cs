using System.Runtime.CompilerServices;

namespace NineDigit.Cloneable;

internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T>
{
    public static ReferenceEqualityComparer<T> Instance = new();

    public bool Equals(T x, T y)
        => ReferenceEquals(x, y);

    public int GetHashCode(T obj)
        => RuntimeHelpers.GetHashCode(obj);
}