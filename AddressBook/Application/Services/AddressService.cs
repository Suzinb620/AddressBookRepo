using Application.DataTransferObjects;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Bson;

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
        public ObjectId? FindObjectId(string id)
        {
            return _addressRepository.FindObjectId(id);
        }

        public async Task<ObjectId?> FindObjectIdAsync(string id)
        {
            return await _addressRepository.FindObjectIdAsync(id);
        }

        public AddressDto? FindByObjectId(ObjectId objectId)
        {
            var address = _addressRepository.FindByObjectId(objectId);
            return address == null ? null : new AddressDto(address.Id, address.City, address.Street, address.HouseNumber);
        }

        public async Task<AddressDto?> FindByObjectIdAsync(ObjectId objectId)
        {
            var address = await _addressRepository.FindByObjectIdAsync(objectId);
            return address == null ? null : new AddressDto(address.Id, address.City, address.Street, address.HouseNumber);
        }

        public AddressDto? GetLastAdded()
        {
            var address = _addressRepository.GetLastAdded();
            return address == null ? null : new AddressDto(address.Id, address.City, address.Street, address.HouseNumber);
        }
        
        public async Task<AddressDto?> GetLastAddedAsync()
        {
            var address = await _addressRepository.GetLastAddedAsync();
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
        
        public async Task<IEnumerable<AddressDto>?> GetByCityAsync(string city)
        {
            var addresses = await _addressRepository.GetByCityAsync(city);
            if (addresses == null) return null;

            var output = new HashSet<AddressDto>();

            var tasks = addresses
                .Select(address => Task.Run(() => output
                    .Add(new AddressDto(address.Id, address.City, address.Street, address.HouseNumber))))
                .ToList();

            await Task.WhenAll(tasks);
            return output;
        }

        public AddressDto? Add(CreateAddressDto address)
        {
           var created = _addressRepository.Add(new Address(address.City, address.Street, address.HouseNumber));

           return created == null ? null : new AddressDto(created.Id, created.City, created.Street, created.HouseNumber);
        }
        
        public async Task<AddressDto?> AddAsync(CreateAddressDto address)
        {
            var created = await _addressRepository.AddAsync(new Address(address.City, address.Street, address.HouseNumber));

            return created == null ? null : new AddressDto(created.Id, created.City, created.Street, created.HouseNumber);
        }

        public bool DeleteByObjectId(ObjectId objectId)
        {
            return _addressRepository.DeleteByObjectId(objectId);
        }
        
        public async Task<bool> DeleteByObjectIdAsync(ObjectId objectId)
        {
            return await _addressRepository.DeleteByObjectIdAsync(objectId);
        }

        public AddressDto? Put(AddressDto addressDto)
        {
            var response = _addressRepository.Put(new Address(addressDto.City, addressDto.Street, addressDto.HouseNumber));
            
            return response == null ? null : new AddressDto(response.Id, response.City, response.Street, response.HouseNumber);
        }

        public async Task<AddressDto?> PutAsync(AddressDto addressDto)
        {
            var response = await _addressRepository.AddAsync(new Address(addressDto.City, addressDto.Street, addressDto.HouseNumber));

            return response == null ? null : new AddressDto(response.Id, response.City, response.Street, response.HouseNumber);
        }
    }
}