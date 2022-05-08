using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        //Variables:
        private readonly ISet<Address> _addresses = new SortedSet<Address>();
        private static readonly string _path = Directory.GetCurrentDirectory() + "/AddressBookData.json";

        //Constructors:
        public AddressRepository()
        {
            if (File.Exists(_path))
                if(new FileInfo(_path).Length != 0)
                    _addresses = (JsonSerializer.Deserialize<ISet<Address>>(File.ReadAllText(_path))!);
        }

        //Methods:
        public Address? GetLastAdded() => _addresses?.LastOrDefault();

        public IEnumerable<Address>? GetByCity(string city)
        {
            if (!_addresses.Any()) return null;

            var output = new SortedSet<Address>();
            foreach (var address in _addresses)
                if (address.City == city)
                    output.Add(address);

            return output.Any() ? output : null;
        }

        public Address? Add(Address address)
        {
            if (!_addresses.Any()) address.Id = 1;
            else address.Id = _addresses.Count + 1;

            if (!_addresses.Add(address)) return null;
            File.Delete(_path);
            File.AppendAllText(_path, JsonSerializer.Serialize(_addresses, new JsonSerializerOptions{WriteIndented = true}));
            return address;
        }
    }
}