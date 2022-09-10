using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        Address? GetLastAdded();
        Task<Address?> GetLastAddedAsync();
        IEnumerable<Address>? GetByCity(string city);
        Task<IEnumerable<Address>?> GetByCityAsync(string city);
        Address? Add(Address address);
        Task<Address?> AddAsync(Address address);
    }
}