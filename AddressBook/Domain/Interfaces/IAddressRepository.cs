using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> GetAll();
        IEnumerable<Address> GetByCity(string city);
        void Add(Address address);
        void Update(Address address);
        void Delete(Address address);
    }
}
