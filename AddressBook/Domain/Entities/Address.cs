
namespace Domain.Entities
{
    public class Address : IComparable<Address>
    {
        //Variables:
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        private DateTime Created { get; init; }

        //Constructors:
        public Address(int id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id, city, street, houseNumber);
            Created = DateTime.UtcNow;
        }

        //Methods:
        public int CompareTo(Address? address) => address == null ? 1 : Created.CompareTo(address.Created);

        public override bool Equals(object? obj)
        {
            if (obj is not Address address) return false;
            return City == address.City && Street == address.Street && HouseNumber == address.HouseNumber;
        }

        public override int GetHashCode() => HashCode.Combine(City, Street, HouseNumber);
    }
}