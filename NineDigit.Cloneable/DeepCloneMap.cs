using System.Diagnostics.CodeAnalysis;

namespace NineDigit.Cloneable
{
    public class DeepCloneMap
    {
        public DeepCloneMap()
            : this(new DeepCloneMapEntries())
        {
        }
        
        public DeepCloneMap(DeepCloneMap map)
        {
            if (map is null)
                throw new ArgumentNullException(nameof(map));

            this.Entries = new DeepCloneMapEntries(map.Entries);
        }
        
        public DeepCloneMap(DeepCloneMapEntries entries)
        {
            this.Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }
        
        private DeepCloneMapEntries Entries { get; }

        public void Map(object from, object to)
        {
            var entry = new DeepCloneMapEntry(from, to);
            this.Entries.Add(entry);
        }
        
        public bool TryGetValue(object from, [NotNullWhen(true)] out object? to)
        {
            foreach (var entry in this.Entries)
            {
                if (entry.TryMap(from, out to))
                    return true;
            }

            to = default;
            return false;
        }
    }
}