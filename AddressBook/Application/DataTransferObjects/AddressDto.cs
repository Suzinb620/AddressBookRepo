using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace Application.DataTransferObjects
{
    public class AddressDto
    {
        //Variables:
        public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        //Constructors:
        public AddressDto(ObjectId id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id.ToString(), city, street, houseNumber);
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