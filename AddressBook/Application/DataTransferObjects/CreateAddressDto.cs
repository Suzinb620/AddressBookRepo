using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{ public class CreateAddressDto
    {
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Street { get; set; }

        [Required]
        [Range(1, 1000)]
        public int HouseNumber { get; set; }

        public CreateAddressDto(string city, string street, int houseNumber)
        {
            (City, Street, HouseNumber) = (city, street, houseNumber);
        }
    }
}
