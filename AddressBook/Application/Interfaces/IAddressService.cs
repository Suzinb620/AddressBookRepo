using Application.DataTransferObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Interfaces
{
    public interface IAddressService
    {
        ObjectId? FindObjectId(string id);
        Task<ObjectId?> FindObjectIdAsync(string id);
        AddressDto? FindByObjectId(ObjectId objectId);
        Task<AddressDto?> FindByObjectIdAsync(ObjectId objectId);
        AddressDto? GetLastAdded();
        Task<AddressDto?> GetLastAddedAsync();        
        IEnumerable<AddressDto>? GetByCity(string? city);
        Task<IEnumerable<AddressDto>?> GetByCityAsync(string? city);
        AddressDto? Add(CreateAddressDto addressDto);
        Task<AddressDto?> AddAsync(CreateAddressDto addressDto);
        bool DeleteByObjectId(ObjectId objectId);
        Task<bool> DeleteByObjectIdAsync(ObjectId objectId);
        ReplaceOneResult? Put(AddressDto addressDto);
        Task<ReplaceOneResult?> PutAsync(AddressDto addressDto);
        UpdateResult? Patch(AddressDto addressDto);
        Task<UpdateResult?> PatchAsync(AddressDto addressDto);
    }
}
