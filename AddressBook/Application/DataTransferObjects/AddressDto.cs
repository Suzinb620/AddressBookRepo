using Domain.Entities;

namespace Application.DataTransferObjects
{
    public class AddressDto : IComparable<AddressDto>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public AddressDto(int id, string city, string street, int houseNumber)
        {
            (Id, City, Street, HouseNumber) = (id, city, street, houseNumber);
        }

        public int CompareTo(AddressDto? address)
        {
            if (address == null) return 1;
            return City == address.City && Street == address.Street && HouseNumber == address.HouseNumber ? 0 : -1;
        }
    }
}
