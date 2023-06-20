using System.Diagnostics.CodeAnalysis;

namespace NineDigit.Cloneable;

public sealed class DeepCloneMapEntry : IEquatable<DeepCloneMapEntry>
{
    private static readonly IEqualityComparer<object> EqualityComparer = ReferenceEqualityComparer<object>.Instance;
    
    public DeepCloneMapEntry(object from, object to)
    {
        this.From = from ?? throw new ArgumentNullException(nameof(from));
        this.To = to ?? throw new ArgumentNullException(nameof(to));
    }
        
    public object From { get; }
    public object To { get; }

    public override int GetHashCode()
        => EqualityComparer.GetHashCode(this.From) ^ EqualityComparer.GetHashCode(this.To);

    public bool TryMap(object from, [NotNullWhen(true)] out object? to)
    {
        if (EqualityComparer.Equals(this.From, from))
        {
            to = this.To;
            return true;
        }
        else if (EqualityComparer.Equals(this.To, from))
        {
            to = this.From;
            return true;
        }
        else
        {
            to = null;
            return false;
        }
    }

    public override bool Equals(object? obj)
        => Equals((DeepCloneMapEntry?)obj);

    public bool Equals(DeepCloneMapEntry? other)
        => Equals(this, other);

    public static bool Equals(DeepCloneMapEntry? left, DeepCloneMapEntry? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return (EqualityComparer.Equals(left.From, right.From) && EqualityComparer.Equals(left.To, right.To) ||
                EqualityComparer.Equals(left.From, right.To) && EqualityComparer.Equals(left.To, right.From));
    }
}