using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{ public class CreateAddressDto
    {
        //Variables:
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+\\-?[a-zA-Z]+$")]  //Accepts upper and lower case letters and one hyphen
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+(\\s?[a-zA-Z]+)*$")] //Accepts upper and lower case letters and spaces
        public string Street { get; set; }

        [Required]
        [Range(1, 1000)]
        public int HouseNumber { get; set; }
    }
}
