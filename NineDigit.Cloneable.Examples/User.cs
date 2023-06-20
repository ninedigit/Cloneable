namespace NineDigit.Cloneable.Examples;

class User : IDeepCloneable<User>
{
    public User()
    {
    }

    public User(User user, DeepCloner cloner)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        if (cloner is null)
            throw new ArgumentNullException(nameof(cloner));

        // This line must be called before any other cloning
        cloner.Map(user, this);
        
        this.Name = user.Name;
        this.Address = cloner.Clone(user.Address);
    }
    
    public string Name { get; set; }
    public UserAddress? Address { get; set; }

    public User DeepClone(DeepCloner? deepCloner = null)
        => new(this, DeepCloner.From(deepCloner));
}