using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public IEnumerable<Address> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Address> GetByCity(string city)
        {
            throw new NotImplementedException();
        }

        public void Add(Address address)
        {
            throw new NotImplementedException();
        }

        public void Update(Address address)
        {
            throw new NotImplementedException();
        }

        public void Delete(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
