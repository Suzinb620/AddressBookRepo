using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects
{ public class CreateAddressDto
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+\\-?[a-zA-Z]+$")]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z]+(\\s?[a-zA-Z]+)*$")]
        public string Street { get; set; }

        [Required]
        [Range(1, 1000)]
        public int HouseNumber { get; set; }
    }
}
