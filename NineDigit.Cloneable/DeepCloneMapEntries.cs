namespace NineDigit.Cloneable;

public class DeepCloneMapEntries : HashSet<DeepCloneMapEntry>
{
    public DeepCloneMapEntries()
    {
    }

    public DeepCloneMapEntries(DeepCloneMapEntries entries)
        : base(entries)
    {
    }
}