
namespace Application.DataTransferObjects
{
    public class AddressDto
    {
        //Variables:
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        //Constructors:
        public AddressDto(Guid id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id, city, street, houseNumber);
        }
    }
}