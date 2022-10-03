using Application.DataTransferObjects;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

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
            return address == null ? null : new AddressDto
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            };
        }

        public async Task<AddressDto?> FindByObjectIdAsync(ObjectId objectId)
        {
            var address = await _addressRepository.FindByObjectIdAsync(objectId);
            return address == null ? null : new AddressDto            
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            };
        }

        public AddressDto? GetLastAdded()
        {
            var address = _addressRepository.GetLastAdded();
            return address == null ? null : new AddressDto
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            };
        }
        
        public async Task<AddressDto?> GetLastAddedAsync()
        {
            var address = await _addressRepository.GetLastAddedAsync();
            return address == null ? null : new AddressDto
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            };
        }

        public IEnumerable<AddressDto>? GetByCity(string city)
        {
            var addresses = _addressRepository.GetByCity(city);
            return addresses?.Select(address => new AddressDto
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            });
        }
        
        public async Task<IEnumerable<AddressDto>?> GetByCityAsync(string city)
        {
            var addresses = await _addressRepository.GetByCityAsync(city);
            if (addresses is null)
            {
                return null;
            }

            var output = new List<AddressDto>();

            var tasks = addresses.Select(address => Task.Run(() => output.Add(new AddressDto
            {
                Id = address.Id.ToString(), 
                City = address.City, 
                Street = address.Street, 
                HouseNumber = address.HouseNumber
            })));

            await Task.WhenAll(tasks);
            return output;
        }

        public AddressDto? Add(CreateAddressDto address)
        {
           var created = _addressRepository.Add(new Address
           {
               Id = new ObjectId(),
               City =  address.City,
               Street = address.Street,
               HouseNumber = address.HouseNumber,
               Updated = DateTime.UtcNow,
               Created = DateTime.UtcNow,
               CreatedBy = "admin"
           });

           return created is null ? null : new AddressDto
           {
               Id = created.Id.ToString(), 
               City = created.City, 
               Street = created.Street, 
               HouseNumber = created.HouseNumber
           };
        }
        
        public async Task<AddressDto?> AddAsync(CreateAddressDto address)
        {
            var created = await _addressRepository.AddAsync(new Address
            {
                Id = new ObjectId(),
                City =  address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                CreatedBy = "admin"
            });

            return created is null ? null : new AddressDto
            {
                Id = created.Id.ToString(), 
                City = created.City, 
                Street = created.Street, 
                HouseNumber = created.HouseNumber
            };
        }

        public bool DeleteByObjectId(ObjectId objectId)
        {
            return _addressRepository.DeleteByObjectId(objectId);
        }
        
        public async Task<bool> DeleteByObjectIdAsync(ObjectId objectId)
        {
            return await _addressRepository.DeleteByObjectIdAsync(objectId);
        }

        public ReplaceOneResult? Put(AddressDto addressDto)
        {
            var id = ObjectId.Parse(addressDto.Id);
            var response = _addressRepository.FindByObjectId(id);
            
            return _addressRepository.Put(new Address
            {
                Id = id,
                City =  addressDto.City,
                Street = addressDto.Street,
                HouseNumber = addressDto.HouseNumber,
                Updated = DateTime.UtcNow,
                Created = response?.Created ?? DateTime.Now,
                CreatedBy = response?.CreatedBy ?? "admin"
            });
        }

        public async Task<ReplaceOneResult?> PutAsync(AddressDto addressDto)
        {
            var id = ObjectId.Parse(addressDto.Id);
            var response = await _addressRepository.FindByObjectIdAsync(id);
            
            return await _addressRepository.PutAsync(new Address
            {
                Id = id,
                City =  addressDto.City,
                Street = addressDto.Street,
                HouseNumber = addressDto.HouseNumber,
                Updated = DateTime.UtcNow,
                Created = response?.Created ?? DateTime.Now,
                CreatedBy = response?.CreatedBy ?? "admin"
            });
        }

        public UpdateResult? Patch(AddressDto addressDto)
        {
            var id = ObjectId.Parse(addressDto.Id);
            var response = _addressRepository.FindByObjectId(id);
            
            return _addressRepository.Patch(new Address
            {
                Id = id,
                City =  addressDto.City,
                Street = addressDto.Street,
                HouseNumber = addressDto.HouseNumber,
                Updated = DateTime.UtcNow,
                Created = response?.Created ?? DateTime.Now,
                CreatedBy = response?.CreatedBy ?? "admin"
            });
        }

        public async Task<UpdateResult?> PatchAsync(AddressDto addressDto)
        {
            var id = ObjectId.Parse(addressDto.Id);
            var response = await _addressRepository.FindByObjectIdAsync(id);
            
            return await _addressRepository.PatchAsync(new Address
            {
                Id = id,
                City =  addressDto.City,
                Street = addressDto.Street,
                HouseNumber = addressDto.HouseNumber,
                Updated = DateTime.UtcNow,
                Created = response?.Created ?? DateTime.Now,
                CreatedBy = response?.CreatedBy ?? "admin"
            });
        }
    }
}