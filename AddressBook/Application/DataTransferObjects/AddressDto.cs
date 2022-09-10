
using MongoDB.Bson;

namespace Application.DataTransferObjects
{
    public class AddressDto
    {
        //Variables:
        public ObjectId Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        //Constructors:
        public AddressDto(ObjectId id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id, city, street, houseNumber);
        }

        //Methods:
        public override bool Equals(object? obj)
        {
            if (obj is not AddressDto address) return false;
            return City == address.City && Street == address.Street && HouseNumber == address.HouseNumber;
        }

        public override int GetHashCode() => HashCode.Combine(City, Street, HouseNumber);
    }
}