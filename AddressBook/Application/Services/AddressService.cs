using Application.DataTransferObjects;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        //Variables:
        private readonly IAddressRepository _addressRepository;

        //Constructors:
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        //Methods:
        public AddressDto? GetLastAdded()
        {
            var address = _addressRepository.GetLastAdded();
            return address == null ? null : new AddressDto(address.Id, address.City, address.Street, address.HouseNumber);
        }

        public IEnumerable<AddressDto>? GetByCity(string city)
        {
            var addresses = _addressRepository.GetByCity(city);
            if (addresses == null) return null;

            var output = new HashSet<AddressDto>();
            foreach (var address in addresses)
                output.Add(new AddressDto(address.Id, address.City, address.Street, address.HouseNumber));

            return output;
        }

        public AddressDto? Add(CreateAddressDto address)
        {
           var created = _addressRepository.Add(new Address(address.City, address.Street, address.HouseNumber));

           return created == null ? null : new AddressDto(created.Id, created.City, created.Street, created.HouseNumber);
        }
    }
}