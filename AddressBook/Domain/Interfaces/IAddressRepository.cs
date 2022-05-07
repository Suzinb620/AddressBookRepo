using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        Address? GetLastAdded();
        IEnumerable<Address>? GetByCity(string city);
        Address? Add(Address address);
    }
}
