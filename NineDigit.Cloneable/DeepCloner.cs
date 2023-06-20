using System.Diagnostics.CodeAnalysis;

namespace NineDigit.Cloneable
{
    public class DeepCloner
    {
        public DeepCloner()
            : this(new DeepCloneMap())
        {
        }

        public DeepCloner(DeepCloner deepCloner)
        {
            if (deepCloner is null)
                throw new ArgumentNullException(nameof(deepCloner));

            this.Mappings = new DeepCloneMap(deepCloner.Mappings);
        }
        
        public DeepCloner(DeepCloneMap? map)
        {
            this.Mappings = map ?? new DeepCloneMap();
        }
        
        protected DeepCloneMap Mappings { get; }

        public virtual bool Map<T>(T from, T to)
        {
            if (from is null)
                throw new ArgumentNullException(nameof(from));

            if (to is null)
                throw new ArgumentNullException(nameof(to));

            if (!ReferenceEquals(from, to))
            {
                this.Mappings.Map(from, to);
                return true;
            }

            return false;
        }

        //[return: MaybeNull]
        public IEnumerable<T> Clone<T>(IEnumerable<T> items)
            where T : class, IDeepCloneable<T>
            => items.Select(Clone);

        [return: NotNullIfNotNull("value")]
        public T? Clone<T>(T? value) where T : class, IDeepCloneable<T>
        {
            if (value is null)
            {
                return null;
            }
            else if (this.Mappings.TryGetValue(value, out object? objectClone))
            {
                return (T)objectClone!;
            }
            else
            {
                var valueClone = value.DeepClone(this);

                /*
                 * Note: Nepridávame novú hodnotu, nakoľko v procese klonovania
                 * mohola byť táto hodnota už pridaná.
                 */

                this.Map(value, valueClone);
                return valueClone;
            }
        }

        public static DeepCloner From(DeepCloner? deepCloner) => new(deepCloner?.Mappings ?? new DeepCloneMap());

        // public static void Clone(DeepCloneMap? map, Action<DeepCloner> builder)
        // {
        //     var cloner = new DeepCloner(map);
        //     builder(cloner);
        // }

        // public static T Clone<T>(T source, DeepCloneMap? map, Action<T, T, DeepCloner> builder)
        //     where T : class, new()
        // {
        //     var destination = new T();
        //     var result = CloneTo(source, destination, map, builder);
        //
        //     return result;
        // }

        // public static T CloneTo<T>(T source, T destination, DeepCloneMap? map, Action<T, T, DeepCloner> builder)
        //     where T : class
        // {
        //     var cloner = new DeepCloner(map);
        //     builder(source, destination, cloner);
        //
        //     cloner.Map(source, destination);
        //
        //     return destination;
        // }
    }
}