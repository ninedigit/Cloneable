namespace NineDigit.Cloneable.Examples;

class UserAddress : IDeepCloneable<UserAddress>
{
    public UserAddress()
    {
    }
    
    public UserAddress(UserAddress address, DeepCloner cloner)
    {
        if (address is null)
            throw new ArgumentNullException(nameof(address));

        if (cloner is null)
            throw new ArgumentNullException(nameof(cloner));
        
        // This line must be called before any other cloning
        cloner.Map(address, this);

        this.Street = address.Street;
        this.City = address.City;
        this.User = cloner.Clone(address.User);
    }
    
    public string Street { get; set; }
    public string City { get; set; }
    
    public User? User { get; set; }

    public UserAddress DeepClone(DeepCloner? deepCloner = null)
        => new(this, DeepCloner.From(deepCloner));
}