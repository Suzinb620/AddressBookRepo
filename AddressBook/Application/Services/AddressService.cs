using System.Data;
using System.Reflection.Emit;
using Application.DataTransferObjects;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        //Methods:
        public AddressDto? GetLastAdded()
        {
            var address = _addressRepository.GetLastAdded();

            if (address == null) return null;
            return new AddressDto(address.Id, address.City, address.Street, address.HouseNumber);
        }

        public IEnumerable<AddressDto>? GetByCity(string city)
        {
            var addresses = _addressRepository.GetByCity(city);
            var output = new SortedSet<AddressDto>();

            if (addresses == null) return null;

            foreach (var address in addresses)
                output.Add(new AddressDto(address.Id, address.City, address.Street, address.HouseNumber));

            return output;
        }

        public AddressDto? Add(CreateAddressDto addressDto)
        {
            if (string.IsNullOrWhiteSpace(addressDto.City)) throw new Exception("Address cannot have an empty city");
            if (string.IsNullOrWhiteSpace(addressDto.Street)) throw new Exception("Address cannot have an empty street");
            if (addressDto.HouseNumber == 0) throw new Exception("Address cannot have 0 as the house number");

            return _addressRepository.Add(new Address(0, addressDto.City, addressDto.Street, addressDto.HouseNumber)) == null ? null : GetLastAdded();
        }
    }
}