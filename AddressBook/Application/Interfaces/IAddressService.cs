using Application.DataTransferObjects;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        AddressDto? GetLastAdded();
        Task<AddressDto?> GetLastAddedAsync();        
        IEnumerable<AddressDto>? GetByCity(string? city);
        Task<IEnumerable<AddressDto>?> GetByCityAsync(string? city);
        AddressDto? Add(CreateAddressDto addressDto);
        Task<AddressDto?> AddAsync(CreateAddressDto addressDto);
    }
}
