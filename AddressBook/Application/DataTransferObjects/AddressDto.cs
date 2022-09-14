using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace Application.DataTransferObjects
{
    public class AddressDto
    {
        //Variables:
        [Required]
        [RegularExpression("/^[a-f\\d]{24}$/i")] // Validate MongoDb ObjectId
        public string Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+\\-?[a-zA-Z]+$")]  // Accepts upper and lower case letters and one hyphen
        public string City { get; set; }
        
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+(\\s?[a-zA-Z]+)*$")] // Accepts upper and lower case letters and spaces
        public string Street { get; set; }
        
        [Required]
        [Range(1, 1000)]
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