
namespace Domain.Entities
{
    public class Address
    {
        //Variables:
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public DateTime Created { get; init; }

        //Constructors:
        public Address(string city, string street, int houseNumber)
        {
            (City, Street, HouseNumber) = (city, street, houseNumber);
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        //Methods:
        public override bool Equals(object? obj)
        {
            if (obj is not Address address) return false;
            return City == address.City && Street == address.Street && HouseNumber == address.HouseNumber;
        }

        public override int GetHashCode() => HashCode.Combine(City, Street, HouseNumber);
    }
}