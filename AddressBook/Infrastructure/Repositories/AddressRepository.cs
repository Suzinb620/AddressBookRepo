using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        //Variables:
        private readonly ISet<Address> _addresses = new HashSet<Address>();
        private readonly string _path = Directory.GetCurrentDirectory() + "/AddressBookData.json";

        //Constructors:
        public AddressRepository(ISet<Address>? data = null)
        {
            if (data != null) _addresses = data;
            else
                if (File.Exists(_path))
                    if (new FileInfo(_path).Length != 0)
                        _addresses = (JsonSerializer.Deserialize<ISet<Address>>(File.ReadAllText(_path))!);
        }

        //Methods:
        public Address? GetLastAdded() => _addresses.MaxBy(address => address.Created);

        public IEnumerable<Address>? GetByCity(string city)
        {
            if (!_addresses.Any()) return null;

            var output = new HashSet<Address>();
            foreach (var address in _addresses)
                if (address.City == city)
                    output.Add(address);

            return output.Any() ? output : null;
        }

        public Address? Add(Address address)
        {
            if (!_addresses.Add(address)) return null;

            File.Delete(_path);
            File.AppendAllText(_path, JsonSerializer.Serialize(_addresses, new JsonSerializerOptions{WriteIndented = true}));
            return address;
        }
    }
}