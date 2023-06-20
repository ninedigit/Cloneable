namespace NineDigit.Cloneable;

public static class EnumerableExtensions
{
    public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> self)
        where T : class, IDeepCloneable<T>
        => self.Select(i => i?.DeepClone());

    public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> self, DeepCloner cloner)
        where T : class, IDeepCloneable<T>
    {
        if (cloner is null)
            throw new ArgumentNullException(nameof(cloner));

        return self.Select(i => i != null ? cloner.Clone(i) : null);
    }
}