
namespace Application.DataTransferObjects
{
    public class AddressDto : IComparable<AddressDto>
    {
        //Variables:
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        //Constructors:
        public AddressDto(int id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id, city, street, houseNumber);
        }

        //Methods:
        public int CompareTo(AddressDto? address) => address == null ? 1 : Id.CompareTo(address.Id);
    }
}