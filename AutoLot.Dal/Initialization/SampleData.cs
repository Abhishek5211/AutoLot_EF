namespace AutoLot.Dal.Initialization;

public static class SampleData
{
    public static List<Customer> Customers => new()
    {
        new() { Id = 1, PersonalInformation = new() { FirstName = "Dave", LastName = "Brenner" } },
        new() { Id = 2, PersonalInformation = new() { FirstName = "Matt", LastName = "Walton" } },
        new() { Id = 3, PersonalInformation = new() { FirstName = "Steve", LastName = "Hagen" } },
        new() { Id = 4, PersonalInformation = new() { FirstName = "Pat", LastName = "Walton" } },
        new() { Id = 5, PersonalInformation = new() { FirstName = "Bad", LastName = "Customer" } },
    };
    public static List<Make> Makes => new()
    {
        new() { Id = 1, Name = "VW" },
        new() { Id = 2, Name = "Ford" },
        new() { Id = 3, Name = "Saab" },
        new() { Id = 4, Name = "Yugo" },
        new() { Id = 5, Name = "BMW" },
        new() { Id = 6, Name = "Pinto" },
    };

    public static List<Driver> Drivers => new()
    {
        new() { Id = 1, PersonalInformation = new() { FirstName = "Fred", LastName = "Flinstone" } },
        new() { Id = 2, PersonalInformation = new() { FirstName = "Barney", LastName = "Rubble" } }
    };

    public static List<Car> Inventory => new()
    {
        new() { Id = 1, MakeId = 1, Color = "Black", NickName = "Zippy" },
        new() { Id = 2, MakeId = 2, Color = "Rust", NickName = "Rusty" },
        new() { Id = 3, MakeId = 3, Color = "Black", NickName = "Mel" },
        new() { Id = 4, MakeId = 4, Color = "Yellow", NickName = "Clunker" },
        new() { Id = 5, MakeId = 5, Color = "Black", NickName = "Bimmer" },
        new() { Id = 6, MakeId = 5, Color = "Green", NickName = "Hank" },
        new() { Id = 7, MakeId = 5, Color = "Pink", NickName = "Pinky" },
        new() { Id = 8, MakeId = 6, Color = "Black", NickName = "Pete" },
        new() { Id = 9, MakeId = 4, Color = "Brown", NickName = "Brownie" },
        new() { Id = 10, MakeId = 1, Color = "Rust", NickName = "Lemon", IsDrivable = false },
    };
    public static List<Radio> Radios => new()
    {
        new() { Id = 1, CarId = 1, HasSubWoofers = true, RadioId = "SuperRadio 1", HasTweeters = true },
        new() { Id = 2, CarId = 2, HasSubWoofers = true, RadioId = "SuperRadio 2", HasTweeters = true },
        new() { Id = 3, CarId = 3, HasSubWoofers = true, RadioId = "SuperRadio 3", HasTweeters = true },
        new() { Id = 4, CarId = 4, HasSubWoofers = true, RadioId = "SuperRadio 4", HasTweeters = true },
        new() { Id = 5, CarId = 5, HasSubWoofers = true, RadioId = "SuperRadio 5", HasTweeters = true },
        new() { Id = 6, CarId = 6, HasSubWoofers = true, RadioId = "SuperRadio 6", HasTweeters = true },
        new() { Id = 7, CarId = 7, HasSubWoofers = true, RadioId = "SuperRadio 7", HasTweeters = true },
        new() { Id = 8, CarId = 8, HasSubWoofers = true, RadioId = "SuperRadio 8", HasTweeters = true },
        new() { Id = 9, CarId = 9, HasSubWoofers = true, RadioId = "SuperRadio 9", HasTweeters = true },
        new() { Id = 10, CarId = 10, HasSubWoofers = true, RadioId = "SuperRadio 10", HasTweeters = true },
    };
    public static List<CarDriver> CarsAndDrivers => new()
    {
        new() { Id = 1, CarId = 1, DriverId = 1 },
        new() { Id = 2, CarId = 2, DriverId = 2 }
    };

    public static List<Order> Orders => new()
    {
        new() { Id = 1, CustomerId = 1, CarId = 5 },
        new() { Id = 2, CustomerId = 2, CarId = 1 },
        new() { Id = 3, CustomerId = 3, CarId = 4 },
        new() { Id = 4, CustomerId = 4, CarId = 7 },
        new() { Id = 5, CustomerId = 5, CarId = 10 },
    };

    public static List<CreditRisk> CreditRisks => new()
    {
        new()
        {
            Id = 1,
            CustomerId = Customers[4].Id,
            PersonalInformation = new()
            {
                FirstName = Customers[4].PersonalInformation.FirstName,
                LastName = Customers[4].PersonalInformation.LastName
            }
        }
    };
}
