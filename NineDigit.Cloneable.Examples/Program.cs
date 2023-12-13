// See https://aka.ms/new-console-template for more information

using NineDigit.Cloneable.Examples;
using Xunit;

var user = new User
{
    Name = "Joe"
};

var userAddress = new UserAddress
{
    Street = "Random Street 917",
    City = "Future City"
};

user.Address = userAddress;
userAddress.User = user;

var userClone = user.DeepClone();
var addressClone = userAddress.DeepClone();

Assert.NotNull(userClone.Address);
Assert.Equal(userClone, userClone.Address.User);
Assert.NotNull(userClone.Address.User);
Assert.Equal(userClone.Address, userClone.Address.User.Address);

Assert.NotNull(addressClone.User);
Assert.Equal(addressClone, addressClone.User.Address);
Assert.NotNull(addressClone.User.Address);
Assert.Equal(addressClone.User, addressClone.User.Address.User);

Console.ReadKey();