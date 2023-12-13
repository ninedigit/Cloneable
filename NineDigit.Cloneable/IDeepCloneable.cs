using System.Diagnostics.CodeAnalysis;

namespace NineDigit.Cloneable
{
    public interface IDeepCloneable
    {
        object DeepClone(DeepCloner? deepCloner = null);
    }

    public interface IDeepCloneable<T> : IDeepCloneable
    {
        [return: NotNull]
        new T DeepClone(DeepCloner? deepCloner = null);

#if NETSTANDARD2_1
        object IDeepCloneable.DeepClone(DeepCloner? deepCloner)
            => this.DeepClone(deepCloner);
#endif
    }
}