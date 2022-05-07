using Application.DataTransferObjects;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        AddressDto? GetLastAdded();
        IEnumerable<AddressDto>? GetByCity(string city);
        AddressDto? Add(CreateAddressDto addressDto);
    }
}
