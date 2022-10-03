using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        ObjectId? FindObjectId(string id);
        Task<ObjectId?> FindObjectIdAsync(string id);
        Address? FindByObjectId(ObjectId objectId);
        Task<Address?> FindByObjectIdAsync(ObjectId objectId);
        Address? GetLastAdded();
        Task<Address?> GetLastAddedAsync();
        IEnumerable<Address>? GetByCity(string city);
        Task<IEnumerable<Address>?> GetByCityAsync(string city);
        Address? Add(Address address);
        Task<Address?> AddAsync(Address address);
        Task<bool> DeleteByObjectIdAsync(ObjectId objectId);
        bool DeleteByObjectId(ObjectId objectId);
        ReplaceOneResult? Put(Address address);
        Task<ReplaceOneResult?> PutAsync(Address address);
        UpdateResult? Patch(Address address);
        Task<UpdateResult?> PatchAsync(Address address);
    }
}